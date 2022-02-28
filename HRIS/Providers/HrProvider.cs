﻿using HRIS.Data;
using HRIS.IProviders;
using HRIS.Models;
using HRIS.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRIS.Providers
{
    public class HrProvider : IHrProvider
    {
        private readonly HrDbContext _context;

        public HrProvider(HrDbContext context)
        {
            _context = context;
        }

		public ReturnData<string> SaveWorkFlowDocument(WfDocVm wfDoc)
		{
			try
			{
				wfDoc.Document = wfDoc?.Document ?? "";
				var route = _context.WorkFlowRoutes.Include(r => r.WorkFlowRouteDetails)
					.FirstOrDefault(r => !(bool)r.Closed && r.Document.ToUpper().Equals(wfDoc.Document.ToUpper()));
				if (route == null)
					return new ReturnData<string>
					{
						Success = false,
						Message = $"Sorry, {wfDoc.Document} not supported"
					};
				var details = new List<WorkFlowDocumentDetail>();
				foreach (var routeDetail in route.WorkFlowRouteDetails)
				{
					routeDetail.Approver = routeDetail?.Approver ?? "";
					var approver = _context.WorkFlowApprovers.Include(a => a.WorkFlowApproverDetails)
						.FirstOrDefault(a => a.Title.ToUpper().Equals(routeDetail.Approver.ToUpper()));
					if (approver == null)
						return new ReturnData<string>
						{
							Success = false,
							Message = $"Sorry, {wfDoc.Document} approver not found"
						};

					foreach (var approverDetail in approver.WorkFlowApproverDetails)
					{
						details.Add(new WorkFlowDocumentDetail
						{
							Approver = approver?.Title ?? "",
							Level = routeDetail.Level,
							UserCode = approverDetail.UserCode,
							Status = "",
							Reason = "",
							CreatedDate = wfDoc.CreatedDate
						});
					}
				}

				var document = new WorkFlowDocument
				{
					No = wfDoc.DocNo,
					Type = wfDoc.Document,
					Description = wfDoc.Description,
					UserRef = wfDoc.UserRef,
					Personnel = wfDoc.Personnel,
					CreatedDate = wfDoc.CreatedDate,
					WorkFlowDocumentDetails = details
				};

				_context.WorkFlowDocuments.Add(document);
				return new ReturnData<string>
				{
					Success = true,
					Message = $"{wfDoc.DocNo} saved successfully"
				};
			}
			catch (Exception ex)
			{
				return new ReturnData<string>
				{
					Success = false,
					Message = "Sorry, An error occurred"
				};
			}
		}

		public ReturnData<dynamic> GetEntitledLeave(EntiledLeaveVm entiledLeave)
		{
			try
			{
				var periodResponse = GetCurrentPeriod();
				if (!periodResponse.Success)
					return new ReturnData<dynamic>
					{
						Success = periodResponse.Success,
						Message = periodResponse.Message
					};

				if (string.IsNullOrEmpty(entiledLeave.EmployeeNo))
					return new ReturnData<dynamic>
					{
						Success = false,
						Message = "Kindly provide employee No"
					};
				periodResponse.Data.Name = periodResponse.Data?.Name ?? "";
				entiledLeave.EmployeeNo = entiledLeave?.EmployeeNo ?? "";
				entiledLeave.LeaveType = entiledLeave?.LeaveType ?? "";
				var entilements = _context.LeaveEntilements
					.Where(e => e.EmpNo.ToUpper().Equals(entiledLeave.EmployeeNo.ToUpper())
					&& e.LeaveType.ToUpper().Equals(entiledLeave.LeaveType.ToUpper())
					&& e.Period.ToUpper().Equals(periodResponse.Data.Name.ToUpper()));

				var appliedLeave = _context.LeaveApplications
					.Where(a => a.EmployeeNo.ToUpper().Equals(entiledLeave.EmployeeNo.ToUpper())
					&& a.Type.ToUpper().Equals(entiledLeave.LeaveType.ToUpper())
					&& a.Period.ToUpper().Equals(periodResponse.Data.Name.ToUpper())
					);

				var entitledDays = entilements.Select(e => e.Days).Sum();
				var appliedDays = appliedLeave.Select(a => a.Days).Sum();
				return new ReturnData<dynamic>
				{
					Success = true,
					Data = new
					{
						entitledDays,
						appliedDays,
						remainingDays = entitledDays - appliedDays
					}
				};
			}
			catch (Exception ex)
			{
				return new ReturnData<dynamic>
				{
					Success = false,
					Message = "Sorry, An error occurred"
				};
			}
		}

		private ReturnData<LeavePeriod> GetCurrentPeriod()
		{
			try
			{
				var period = _context.LeavePeriods.FirstOrDefault(p => p.StartDate < DateTime.Today && p.EndDate > DateTime.Today);
				if (period == null)
					return new ReturnData<LeavePeriod>
					{
						Success = false,
						Message = "Sorry, Leave period not found"
					};
				return new ReturnData<LeavePeriod>
				{
					Success = true,
					Data = period
				};
			}
			catch (Exception ex)
			{
				return new ReturnData<LeavePeriod>
				{
					Success = false,
					Message = "Sorry, An error occurred"
				};
			}
		}

		public ReturnData<dynamic> CalculateLeaveDays(LeaveApplication application)
		{
			try
			{
				if (application.StartDate > application.EndDate)
					return new ReturnData<dynamic>
					{
						Success = false,
						Message = "Sorry, End date must be greater than start date"
					};

				var weekEnds = _context.WorkingDays.Where(d => d.Type.Equals("Non-working Day"))
					.Select(d => d.Name.ToUpper()).ToList();

				var periodResponse = GetCurrentPeriod();
				if (!periodResponse.Success)
					return new ReturnData<dynamic>
					{
						Success = periodResponse.Success,
						Message = periodResponse.Message
					};

				periodResponse.Data.Name = periodResponse.Data?.Name ?? "";
				var holidays = _context.Holidays.Where(h => h.Period.ToUpper().Equals(periodResponse.Data.Name.ToUpper()))
					.Select(h => h.Date).ToList();
				application.Days = (application.EndDate - application.StartDate).GetValueOrDefault().Days;
				application.Type = application?.Type ?? "";
				var workDayDates = new List<DateTime>();
				var leaveType = _context.LeaveTypes.FirstOrDefault(t => t.Name.ToUpper().Equals(application.Type.ToUpper()));
				if (leaveType == null)
					return new ReturnData<dynamic>
					{
						Success = false,
						Message = "Sorry, Leave type not found"
					};
				for (var date = application.StartDate.GetValueOrDefault(); date <= application.EndDate.GetValueOrDefault(); date = date.AddDays(1))
				{
					if ((bool)leaveType.IsCalenderDays || !weekEnds.Contains(date.DayOfWeek.ToString().ToUpper()))
						workDayDates.Add(date);
					if ((bool)leaveType.ExcludeHolidays && holidays.Contains(date))
						workDayDates.Remove(date);
				}

				application.Days = workDayDates.Count;
				application.Days = AddHalfDays(application);
				application.StartDate = application.StartDate.GetValueOrDefault().AddDays(1);
				application.EndDate = application.EndDate.GetValueOrDefault().AddDays(1);
				return new ReturnData<dynamic>
				{
					Success = true,
					Data = new
					{
						application
					}
				};
			}
			catch (Exception ex)
			{
				return new ReturnData<dynamic>
				{
					Success = false,
					Message = "Sorry, An error occurred"
				};
			}
		}

		private double AddHalfDays(LeaveApplication application)
		{
			if ((application.StartTime == "AM" && application.EndTime == "AM")
				|| (application.StartTime == "PM" && application.EndTime == "PM"))
				return (double)application.Days - 0.5;

			if ((application.StartDate == application.EndDate)
				&& (application.StartTime == "PM" && application.EndTime == "AM"))
				return -0.5;

			if ((application.StartDate != application.EndDate)
				&& (application.StartTime == "PM" && application.EndTime == "AM"))
				return (double)application.Days - 1;

			return (double)application.Days;
		}
	}
}

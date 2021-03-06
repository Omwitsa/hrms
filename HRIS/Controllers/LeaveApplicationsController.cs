using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;
using HRIS.Utilities;
using HRIS.Constants;
using HRIS.IProviders;
using HRIS.ViewModel;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HRIS.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly INotyfService _notyf;
        private IHrProvider _hrProvider;
        private readonly HrDbContext _context;
        private Utility utility = new Utility();

        public LeaveApplicationsController(HrDbContext context, IHrProvider hrProvider, INotyfService notyf)
        {
            _context = context;
            _hrProvider = hrProvider;
            _notyf = notyf;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveApplications.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .FirstOrDefaultAsync(m => m.LeaveNo == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            ViewBag.dayTimes = new SelectList(ArrValues.DayTimes);
            var employees = _context.Employees.Where(d => !d.Terminated)
               .Select(d => new Employee
               {
                   EmployeeNo = d.EmployeeNo,
                   Name = d.Name
               }).ToList();
            ViewBag.employees = new SelectList(employees, "EmployeeNo", "Name");

            var leaveTypes = _context.LeaveTypes.Where(d => !d.Closed)
               .Select(d => new LeaveType
               {
                   Name = d.Name
               }).ToList();
            ViewBag.leaveTypes = new SelectList(leaveTypes, "Name", "Name");

            var prefix = "LVE";
            var recentEntry = _context.LeaveApplications.ToList()
                .OrderByDescending(s => Convert.ToInt32(s.LeaveNo.Substring(prefix.Length)))
                .FirstOrDefault();
            var no = $"{prefix}001";
            if (recentEntry != null)
                no = utility.GenerateNo(prefix, recentEntry.LeaveNo);
            var leaveApplication = new LeaveApplication
            {
                LeaveNo = no
            };
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveNo,EmployeeNo,StartDate,EndDate,StartTime,EndTime,Days,Type,Period,Notes,Status,Personnel,CreatedDate,ModifiedDate")] LeaveApplication leaveApplication)
        {
            leaveApplication.Status = "Pending";
            string message = "";
            if (string.IsNullOrEmpty(leaveApplication.EmployeeNo))
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide employee";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(leaveApplication);
            }

            if (leaveApplication.StartDate == null)
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide start date";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(leaveApplication);
            }

            if (leaveApplication.EndDate == null)
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide end date";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(leaveApplication);
            }

            if (leaveApplication.StartDate > leaveApplication.EndDate)
            {
                ViewBag.success = false;
                message = "Sorry, End date must be greater than start date";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(leaveApplication);
            }

            if(leaveApplication.Days <= 0)
            {
                ViewBag.success = false;
                message = "Sorry, Leave days must be greater than 0";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(leaveApplication);
            }

            var doc = "Leave Application";
            var wfDoc = new WfDocVm
            {
                DocNo = leaveApplication.LeaveNo,
                Document = doc,
                Description = $"Leave Type: {leaveApplication.Type}, Days: {leaveApplication.Days}, From: {leaveApplication.StartDate} to: {leaveApplication.EndDate}",
                UserRef = leaveApplication.EmployeeNo,
                Personnel = leaveApplication.Personnel,
                CreatedDate = leaveApplication.CreatedDate
            };

            var docResp = _hrProvider.SaveWorkFlowDocument(wfDoc);
            if (!docResp.Success)
            {
                ViewBag.success = false;
                TempData["message"] = docResp.Message;
                _notyf.Error(docResp.Message);
                return View(leaveApplication);
            }

            if (ModelState.IsValid)
            {
                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();
                _notyf.Success("Leave saved successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(leaveApplication);
        }

        [HttpPost]
        public JsonResult OnleaveTypeChange([FromBody] EntiledLeaveVm entiledLeave)
        {
            var results = _hrProvider.GetEntitledLeave(entiledLeave);
            return Json(results);
        }

        [HttpPost]
        public JsonResult CalculateLeaveDays([FromBody] LeaveApplication application)
        {
            var results = _hrProvider.CalculateLeaveDays(application);
            return Json(results);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LeaveNo,EmployeeNo,StartDate,EndDate,StartTime,EndTime,Days,Type,Period,Notes,Status,Personnel,CreatedDate,ModifiedDate")] LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.LeaveNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.LeaveNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .FirstOrDefaultAsync(m => m.LeaveNo == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            _context.LeaveApplications.Remove(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(string id)
        {
            return _context.LeaveApplications.Any(e => e.LeaveNo == id);
        }
    }
}

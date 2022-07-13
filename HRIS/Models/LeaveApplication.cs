using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeaveApplication
	{
		[Key]
		[Display(Name = "Leave No.")]
		public string LeaveNo { get; set; }
		[Required]
		[Display(Name = "Employee No.")]
		public string EmployeeNo { get; set; }
		[Display(Name = "Start Date")]
		public DateTime? StartDate { get; set; }
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
		[Display(Name = "Start Time")]
		public string StartTime { get; set; }
		[Display(Name = "End Time")]
		public string EndTime { get; set; }
		public double? Days { get; set; }
		[Required]
		public string Type { get; set; }
		public string Period { get; set; }
		public string Notes { get; set; }
		public string Status { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

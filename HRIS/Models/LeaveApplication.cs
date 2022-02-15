using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeaveApplication
	{
		[Key]
		public string LeaveNo { get; set; }
		public string EmployeeNo { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public double? Days { get; set; }
		public string Type { get; set; }
		public string Period { get; set; }
		public string Notes { get; set; }
		public string Status { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

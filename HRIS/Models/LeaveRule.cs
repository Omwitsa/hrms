using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeaveRule
	{
		public Guid Id { get; set; }
		[Required]
		[Display(Name = "Leave Group")]
		public string LeaveGroup { get; set; }
		[Required]
		[Display(Name = "Leave Type")]
		public string LeaveType { get; set; }
		[Display(Name = "Leave Days")]
		public double? LeaveDays { get; set; }
		public string Gender { get; set; }
		[Display(Name = "Start Date")]
		public DateTime? StartDate { get; set; }
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }

	}
}

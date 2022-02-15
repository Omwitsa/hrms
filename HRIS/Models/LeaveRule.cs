using System;

namespace HRIS.Models
{
	public class LeaveRule
	{
		public Guid Id { get; set; }
		public string LeaveGroup { get; set; }
		public string LeaveType { get; set; }
		public double? LeaveDays { get; set; }
		public string Gender { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }

	}
}

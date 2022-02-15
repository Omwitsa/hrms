using System;

namespace HRIS.Models
{
	public class LeaveEntilement
	{
		public Guid Id { get; set; }
		public string Period { get; set; }
		public string EmpNo { get; set; }
		public string LeaveType { get; set; }
		public string Type { get; set; }
		public double? Days { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

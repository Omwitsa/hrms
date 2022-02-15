using System;

namespace HRIS.Models
{
	public class LeaveType
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool? Closed { get; set; }
		public string Notes { get; set; }
		public bool? IsCalenderDays { get; set; }
		public bool? ExcludeHolidays { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

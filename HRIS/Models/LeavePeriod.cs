using System;

namespace HRIS.Models
{
	public class LeavePeriod
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Notes { get; set; }
	}
}

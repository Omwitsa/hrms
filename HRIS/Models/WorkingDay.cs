using System;

namespace HRIS.Models
{
	public class WorkingDay
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Branch { get; set; }
		public string Notes { get; set; }
	}
}

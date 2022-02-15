using System;

namespace HRIS.Models
{
	public class JobCategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool? Closed { get; set; }
	}
}

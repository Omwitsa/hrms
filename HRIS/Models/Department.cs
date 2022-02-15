using System;

namespace HRIS.Models
{
	public class Department
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool IsStudent { get; set; }
		public bool? Closed { get; set; }
		public string Notes { get; set; }
	}
}

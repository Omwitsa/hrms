using System;

namespace HRIS.Models
{
	public class EmpCategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
	}
}

using System;

namespace HRIS.Models
{
	public class Branch
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Closed { get; set; }
	}
}

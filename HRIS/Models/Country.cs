using System;

namespace HRIS.Models
{
	public class Country
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool? Closed { get; set; }
		public string Notes { get; set; }
	}
}

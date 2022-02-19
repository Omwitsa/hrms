using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class WorkingDay
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Type { get; set; }
		public string Branch { get; set; }
		public string Notes { get; set; }
	}
}

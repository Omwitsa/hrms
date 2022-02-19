using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeavePeriod
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Notes { get; set; }
	}
}

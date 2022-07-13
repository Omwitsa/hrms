using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeavePeriod
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Display(Name = "Start Date")]
		public DateTime? StartDate { get; set; }
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
		public string Notes { get; set; }
	}
}

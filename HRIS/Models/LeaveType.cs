using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class LeaveType
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
		[Display(Name = "Is Calender Days")]
		public bool IsCalenderDays { get; set; }
		[Display(Name = "Exclude Holidays")]
		public bool ExcludeHolidays { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

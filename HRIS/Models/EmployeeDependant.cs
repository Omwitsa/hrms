using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class EmployeeDependant
	{
		public Guid Id { get; set; }
		[Required]
		public string EmployeeNo { get; set; }
		[Required]
		public string Names { get; set; }
		public string RelationShip { get; set; }
		public string Gender { get; set; }
		public DateTime? DOB { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string Personnel { get; set; }
	}
}

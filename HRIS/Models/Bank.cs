using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class Bank
	{
		public Guid Id { get; set; }
		[Required]
		public string Code { get; set; }
		[Required]
		public string Name { get; set; }
		public string Branch { get; set; }
		public string TelNo { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
	}
}

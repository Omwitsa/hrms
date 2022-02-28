using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class Employee
	{
		[Key]
		[Display(Name = "Employee No.")]
		public string EmployeeNo { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[Display(Name = "ID No.")]
		public string IdNo { get; set; }
		[Display(Name = "Date Of Birth")]
		public DateTime? DateOfBirth { get; set; }
		[Display(Name = "Hired Date")]
		public DateTime? HiredDate { get; set; }
		public string Location { get; set; }
		[Display(Name = "Leave Group")]
		public string LeaveGroup { get; set; }
		public string Gender { get; set; }
		public string Department { get; set; }
		[Display(Name = "Marital Status")]
		public string MaritalStatus { get; set; }
		public string Spouse { get; set; }
		public string PIN { get; set; }
		public string NSSF { get; set; }
		public string NHIF { get; set; }
		public string Division { get; set; }
		public string Race { get; set; }
		public string Religion { get; set; }
		public string Disability { get; set; }
		public string Country { get; set; }
		public string County { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }
		[Display(Name = "Job Category")]
		public string JobCategory { get; set; }
		[Display(Name = "Employment Category")]
		public string EmploymentCategory { get; set; }
		public string Notes { get; set; }
		[Display(Name = "Personal Email")]
		public string PersonalEmail { get; set; }
		[Display(Name = "Work Email")]
		public string WorkEmail { get; set; }
		public string Cell { get; set; }
		public string TelNo { get; set; }
		public string Web { get; set; }
		public bool Terminated { get; set; }
		[Display(Name = "Termination Date")]
		public DateTime? TerminationDate { get; set; }
		[Display(Name = "Termination Type")]
		public string TerminationType { get; set; }
		[Display(Name = "Termination Notes")]
		public string TerminationNotes { get; set; }
		public string Supervisor { get; set; }
		public string Language { get; set; }
		public string Title { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

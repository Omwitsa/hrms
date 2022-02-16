using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class Employee
	{
		[Key]
		public string EmployeeNo { get; set; }
		public string Name { get; set; }
		public string IdNo { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public DateTime? HiredDate { get; set; }
		public string Location { get; set; }
		public string LeaveGroup { get; set; }
		public string Gender { get; set; }
		public string Department { get; set; }
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
		public string PostalCode { get; set; }
		public string JobCategory { get; set; }
		public string EmploymentCategory { get; set; }
		public string Notes { get; set; }
		public string PersonalEmail { get; set; }
		public string WorkEmail { get; set; }
		public string Cell { get; set; }
		public string TelNo { get; set; }
		public string Web { get; set; }
		public bool Terminated { get; set; }
		public DateTime? TerminationDate { get; set; }
		public string TerminationType { get; set; }
		public string TerminationNotes { get; set; }
		public string Supervisor { get; set; }
		public string Language { get; set; }
		public string Title { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

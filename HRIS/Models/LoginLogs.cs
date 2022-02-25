using System;

namespace HRIS.Models
{
	public class LoginLogs
	{
		public Guid Id { get; set; }
		public string UserCode { get; set; }
		public string Names { get; set; }
		public string EmpNo { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}

using System;

namespace HRIS.Models
{
	public class SystemSetup
	{
		public Guid Id { get; set; }
		public string OrgName { get; set; }
		public string OrgInitial { get; set; }
		public string LogoUrl { get; set; }
		public string PrimaryColor { get; set; }
		public string SecondaryColor { get; set; }
	}
}

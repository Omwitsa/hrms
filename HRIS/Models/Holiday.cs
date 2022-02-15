using System;

namespace HRIS.Models
{
	public class Holiday
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime? Date { get; set; }
		public string Period { get; set; }
		public string Type { get; set; }
		public string Branch { get; set; }
		public bool? Recur { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

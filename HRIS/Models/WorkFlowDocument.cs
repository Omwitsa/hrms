using System;
using System.Collections.Generic;

namespace HRIS.Models
{
	public class WorkFlowDocument
	{
		public Guid Id { get; set; }
		public string No { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string UserRef { get; set; }
		public string LatestApprover { get; set; }
		public string FinalStatus { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public IEnumerable<WorkFlowDocumentDetail> WorkFlowDocumentDetails { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class WorkFlowDocument
	{
		public Guid Id { get; set; }
		public string No { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		[Display(Name = "User Ref")]
		public string UserRef { get; set; }
		[Display(Name = "Latest Approver")]
		public string LatestApprover { get; set; }
		[Display(Name = "Final Status")]
		public string FinalStatus { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public IEnumerable<WorkFlowDocumentDetail> WorkFlowDocumentDetails { get; set; }
	}
}

using System;

namespace HRIS.Models
{
	public class WorkFlowDocumentDetail
	{
		public Guid Id { get; set; }
		public Guid? WorkFlowDocumentId { get; set; }
		public string Approver { get; set; }
		public int? Level { get; set; }
		public string UserCode { get; set; }
		public string Status { get; set; }
		public string Reason { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}

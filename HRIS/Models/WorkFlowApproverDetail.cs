using System;

namespace HRIS.Models
{
	public class WorkFlowApproverDetail
	{
		public Guid Id { get; set; }
		public Guid? WorkFlowApproverId { get; set; }
		public string UserCode { get; set; }
	}
}

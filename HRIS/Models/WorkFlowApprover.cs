using System;
using System.Collections.Generic;

namespace HRIS.Models
{
	public class WorkFlowApprover
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public IEnumerable<WorkFlowApproverDetail> WorkFlowApproverDetails { get; set; }
	}
}

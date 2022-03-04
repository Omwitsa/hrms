using System;
using System.Collections.Generic;

namespace HRIS.Models
{
    public class WorkFlowRoute
	{
		public Guid Id { get; set; }
		public string Document { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public IEnumerable<WorkFlowRouteDetail> WorkFlowRouteDetails { get; set; }
	}
}

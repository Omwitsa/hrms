using System;

namespace HRIS.Models
{
	public class WorkFlowRouteDetail
	{
		public Guid Id { get; set; }
		public Guid? WorkFlowRouteId { get; set; }
		public string Approver { get; set; }
		public int? Level { get; set; }
	}
}

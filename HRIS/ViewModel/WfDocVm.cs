using System;

namespace HRIS.ViewModel
{
	public class WfDocVm
	{
		public string DocNo { get; set; }
		public string Document { get; set; }
		public string Description { get; set; }
		public string UserRef { get; set; }
		public string Personnel { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}

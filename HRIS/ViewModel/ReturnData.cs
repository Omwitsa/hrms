namespace HRIS.ViewModel
{
	public class ReturnData<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}
}

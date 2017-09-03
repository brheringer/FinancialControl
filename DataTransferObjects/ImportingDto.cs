using System;

namespace FinancialControl.DataTransferObjects
{
	public class ImportingDto
		: DataTransferObject
	{
		public bool Success { get; set; }
		public DateTime TimeStamp { get; set; }
		public int QuantityOfImportedEntries { get; set; }
		public string[] ErrorsMessages { get; set; }
	}
}
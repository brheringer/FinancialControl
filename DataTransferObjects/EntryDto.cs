using System;

namespace FinancialControl.DataTransferObjects
{
	public class EntryDto
		: EntityDto
	{
		public DateTime? Date { get; set; }
		public decimal Value { get; set; }
		public string Memo { get; set; }
		public EntityReferenceDto Account { get; set; }
		public EntityReferenceDto Center { get; set; }
	}
}
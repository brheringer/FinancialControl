using System;

namespace FinancialControl.DataTransferObjects
{
	public class EntryTemplateDto
		: EntityDto
	{
		public string Title { get; set; }
		//TODO public string IconURL { get; set; }
		public decimal Value { get; set; }
		public string Memo { get; set; }
		public EntityReferenceDto Account { get; set; }
		public EntityReferenceDto Center { get; set; }
	}
}
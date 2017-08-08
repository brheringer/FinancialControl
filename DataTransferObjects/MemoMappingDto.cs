using System;

namespace FinancialControl.DataTransferObjects
{
	public class MemoMappingDto
		: EntityDto
	{
		public string IncomingMemo { get; set; }
		public string MappedMemo { get; set; }
		public EntityReferenceDto MappedAccount { get; set; }
		public EntityReferenceDto MappedCenter { get; set; }
	}
}
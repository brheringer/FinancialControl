using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class EntriesDto 
		: CollectionDto
	{
		public DateTime? FilterInitialDate { get; set; }
		public DateTime? FilterFinalDate { get; set; }
		public DateTime? FilterExactDate { get; set; }
		public decimal FilterLowerValue { get; set; }
		public decimal FilterHigherValue { get; set; }
		public decimal FilterExactValue { get; set; }
		public string FilterMemo { get; set; }
		public EntityReferenceDto FilterAccount { get; set; }
		public EntityReferenceDto FilterCenter { get; set; }

		public IList<EntryDto> Entries { get; set; }
	}
}
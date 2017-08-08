using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class EntriesTemplatesDto
		: CollectionDto
	{
		public IList<EntryTemplateDto> Templates { get; set; }
	}
}
using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class MemosMappingsDto
		: CollectionDto
	{
		public IList<MemoMappingDto> Mappings { get; set; }
	}
}
using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class EntitiesReferencesDto
		: CollectionDto
	{
		public List<EntityReferenceDto> References { get; set;  }

		public EntitiesReferencesDto()
		{
			this.References = new List<EntityReferenceDto>();
		}
	}
}

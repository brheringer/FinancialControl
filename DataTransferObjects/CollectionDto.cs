using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class CollectionDto
		: DataTransferObject
	{
		public int SearchMaxResults { get; set; }
		public int SearchPage { get; set; }

		public CollectionDto()
			: base()
		{
			this.SearchMaxResults = 25;
			this.SearchPage = 0;
		}
	}
}

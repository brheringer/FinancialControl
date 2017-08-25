using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class AccountTotalizationDto
		: DataTransferObject
	{
		public class DetailDto
		{
			public DateTime Date { get; set; }
			public decimal Value { get; set; }
			public string Memo { get; set; }
			public EntityReferenceDto OriginalEntry { get; set; }
		}

		public string AccountStructure { get; set; }
		public string AccountDescription { get; set; }
		public string CenterStructure { get; set; }
		public string CenterDescription { get; set; }
		public decimal Total { get; set; }
		public List<DetailDto> AnalyticalDetails { get; set; }
	}
}
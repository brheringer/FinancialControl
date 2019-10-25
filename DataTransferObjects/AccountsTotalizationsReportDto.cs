using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class AccountsTotalizationsReportDto
		: CollectionDto
	{
		public DateTime FilterInitialDate { get; set; }
		public DateTime FilterFinalDate { get; set; }
		public EntityReferenceDto FilterAccount { get; set; }
		public EntityReferenceDto FilterResultCenter { get; set; }
		public int FilterAccountLevel { get; set; }
		public int MaxAccountLevel { get; set; }
		public IList<AccountTotalizationDto> AccountsTotalizations { get; set; }
	}
}
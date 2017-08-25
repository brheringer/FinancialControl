using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class AccountsTotalizationsReportDto
		: CollectionDto
	{
		public DateTime FilterInitialDate { get; set; }
		public DateTime FilterFinalDate { get; set; }
		public int MaxAccountLevel { get; set; }
		public IList<AccountTotalizationDto> AccountsTotalizations { get; set; }
	}
}
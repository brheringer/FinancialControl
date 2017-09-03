using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class AccountsDto 
		: CollectionDto
	{
		public string FilterDescription { get; set; }
		public string FilterStructure { get; set; }
		public IList<AccountDto> Accounts { get; set; }
	}
}
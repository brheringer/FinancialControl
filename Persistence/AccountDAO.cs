using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface AccountDAO
		: Dao<Account>
	{
		IList<Account> LoadAll(string username);
		Account LoadFirstMatch(string description, string username);
		IList<Account> Search(string description, string structure, string username);
		IList<Account> SmartSearch(string smartEntry, string username);
	}
}

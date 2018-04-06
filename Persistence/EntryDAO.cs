using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface EntryDAO
		: Dao<Entry>
	{
		IList<Entry> Search(
			DateTime? initialDate, DateTime? finalDate, DateTime? exactDate,
			decimal lowerValue, decimal higherValue, decimal exactValue,
			Account account, ResultCenter center, string memo, string username,
			int searchLimit, int searchPage);
	}
}

using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface ResultCenterDAO
		: Dao<ResultCenter>
	{
		IList<ResultCenter> LoadAll(string username);
		ResultCenter LoadFirstMatch(string description, string username);
		IList<ResultCenter> Search(string code, string description, string username);
		IList<ResultCenter> SmartSearch(string smartEntry, string username);
	}
}

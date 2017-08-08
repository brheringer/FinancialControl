using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface MemoMappingDAO
		: Dao<MemoMapping>
	{
		IList<MemoMapping> LoadAll(string username);
	}
}

using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface EntryTemplateDAO
		: Dao<EntryTemplate>
	{
		IList<EntryTemplate> LoadAll(string username);
	}
}

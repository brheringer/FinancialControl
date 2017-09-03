using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface UserDAO
		: Dao<User>
	{
		IList<User> Search(string filterUserName, string filterName, bool? filterBanished, int maxResults, int page);
		string RetrieveHashedPassword(long userId);
		string RetrieveSalt(long userId);
	}
}

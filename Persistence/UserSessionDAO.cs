using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface UserSessionDAO
		: Dao<UserSession>
	{
		UserSession LoadBy(string userName, string sessionid);
		UserSession LoadBy(User user, string sessionid);
		IList<UserSession> Search(string filterUserName, string filterSessionId);
	}
}

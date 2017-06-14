using System;

namespace FinancialControl.Persistence
{
	public interface DaoFactory
	{
		ResultCenterDAO ResultCenterDAO { get; }
		AccountDAO AccountDAO { get; }
		EntryDAO EntryDAO { get; }
		UserDAO UserDAO { get; }
		UserSessionDAO UserSessionDAO { get; }
	}
}

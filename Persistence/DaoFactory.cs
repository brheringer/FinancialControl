using System;

namespace FinancialControl.Persistence
{
	public interface DAOFactory
	{
		ResultCenterDAO ResultCenterDAO { get; }
		AccountDAO AccountDAO { get; }
		EntryDAO EntryDAO { get; }
		EntryTemplateDAO EntryTemplateDAO { get; }
		MemoMappingDAO MemoMappingDAO { get; }
		UserDAO UserDAO { get; }
		UserSessionDAO UserSessionDAO { get; }
	}
}

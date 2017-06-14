using System;

namespace FinancialControl.Persistence
{
	/// <remarks>
	/// "The main role for the factory is to "inject" 
	/// a repositorylocator instance
	/// when a new manager is created. 
	/// The manager is then responsible for passing 
	/// the repositorylocator to the invoked commands 
	/// but it is not resposible for creating it. 
	/// This approach aligns very well with the NHibernate guidelines."
	/// </remarks>
	public interface TransactionManagerFactory
	{
		TransactionManager Create();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialControl.Persistence;

namespace FinancialControl.WebAPI
{
	/// <remarks>
	/// "The Global context exposes classes and data that can be used by our server components, 
	/// they are request agnostic and are kept alive along with the application. 
	/// A good approach with these global resources and services is not keeping any sort of state if feasible, 
	/// concurrency issues may arise if the design is not correct. 
	/// Locking mechanisms may help with the concurrency 
	/// but may incur in performance degradation."
	/// 
	/// "(we will provide a global context so the server components can gain access to a single instance of the factory)"
	/// </remarks>
	public class GlobalContext
	{
		/// <remarks>
		/// The Transaction Manager factory is an ideal candidate for being stored in the global context. 
		/// The factory class does not manage any state and one single instance satisfy the application 
		/// requirements for the creation of transaction managers even in a multi-request scenario. 
		/// </remarks>
		public TransactionManagerFactory TransactionManagerFactory { get; set; }

		static readonly Object LocatorLock = new object();
		private static GlobalContext _instancia;

		private GlobalContext()
		{
		}

		public static GlobalContext Instance()
		{
			if (_instancia == null)
			{
				lock (LocatorLock)
				{
					// in case of a race scenario ... check again
					if (_instancia == null)
					{
						_instancia = new GlobalContext();
					}
				}
			}
			return _instancia;
		}

	}
}
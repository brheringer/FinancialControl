using System;
using NHibernate;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHTransactionManagerFactory
		: TransactionManagerFactory
	{
		private ISessionFactory SessionFactoryInstance;

		private ISessionFactory SessionFactory
		{
			get
			{
				if (SessionFactoryInstance != null)
					return SessionFactoryInstance;
				SessionFactoryInstance = InitializeSessionFactory();
				return SessionFactoryInstance;
			}
		}

		public string ConfigurationFileName { get; set; }

		private ISessionFactory InitializeSessionFactory()
		{
			NHBootStrapper nhConfiguration = new NHBootStrapper();
			nhConfiguration.ConfigurationFileName = this.ConfigurationFileName;
			return nhConfiguration.NhConfiguration.BuildSessionFactory();
		}

		public TransactionManager Create()
		{
			ISession session = SessionFactory.OpenSession();
			session.FlushMode = FlushMode.Commit;
			return new NHTransactionManager(session);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHTransactionManager
		: TransactionManagerBase
	{
		private readonly ISession SessionInstance;

		public NHTransactionManager(ISession session)
        {
            this.SessionInstance = session;
			this.Factory = new NHibernateDAOFactory(session);
        }

		public override void BeginTransaction()
		{
			base.BeginTransaction();
			if (SessionInstance.Transaction.IsActive) return; //TODO nao deveria deixar dar pau?
			SessionInstance.BeginTransaction();
		}

		public override void Commit()
		{
			base.Commit();
			if (!SessionInstance.Transaction.IsActive) return; //TODO nao deveria deixar dar pau?
			SessionInstance.Transaction.Commit();
		}

		public override void Rollback()
		{
			base.Rollback();
			if (!SessionInstance.Transaction.IsActive) return;
			SessionInstance.Transaction.Rollback();
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposing) return;
			// free managed resources
			if (!this.IsDisposed && this.TransactionIsOpen)
			{
				Rollback();
			}
			Close();
			this.Factory = null;
			this.IsDisposed = true;
		}

		private void Close()
		{
			if (SessionInstance == null) return;
			if (!SessionInstance.IsOpen) return;
			SessionInstance.Close();
		}
	}
}

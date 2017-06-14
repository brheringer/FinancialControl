using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	public class BaseTest
	{
		protected NHTransactionManager TransactionMngrInstance
		{
			get
			{
				return (NHTransactionManager)this._transactionMngr;
			}
		}
		protected TransactionManager _transactionMngr;

		[TestInitialize]
		public void SetUp()
		{
			NHTransactionManagerFactory tmf = new NHTransactionManagerFactory();
			tmf.ConfigurationFileName = "nhibernate_test.cfg.xml";
			this._transactionMngr = tmf.Create();

			var nhBootStrapper = new NHBootStrapper();
			nhBootStrapper.ConfigurationFileName = tmf.ConfigurationFileName;
			nhBootStrapper.SchemaExport.Create(true, true);
		}
	}
}

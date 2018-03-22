using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class EntryDAOTest
		: BaseTest
	{
		private const string USER = "tester";

		[TestMethod]
		public void TestNewUpdate()
		{
			CreateEntry();

			var entries = Search();
			Assert.IsNotNull(entries);
			Assert.AreEqual(1, entries.Count);
			Assert.AreEqual(new DateTime(2017, 2, 28), entries[0].Date);
			Assert.AreEqual(100.00M, entries[0].Value);
			Assert.AreEqual("Test", entries[0].Memo);
			Assert.IsNotNull(entries[0].Account);
			Assert.AreEqual("1", entries[0].Account.Structure);
			Assert.IsNotNull(entries[0].Center);
			Assert.AreEqual("1", entries[0].Center.Code);
		}

		[TestMethod]
		public void TestNewUpdates()
		{
			CreateEntry();
			CreateEntry();
			CreateEntry();

			var entries = Search();
			Assert.IsNotNull(entries);
			Assert.AreEqual(3, entries.Count);
		}

		[TestMethod]
		public void TestLoadUpdate()
		{
			CreateEntry();
			Entry entryAntesUpdate = TransactionMngrInstance.Factory.EntryDAO.Load(1);
			entryAntesUpdate.Memo = "altered";
			TransactionMngrInstance.Factory.EntryDAO.Update(entryAntesUpdate);
			Entry entryDepoisUpdate = TransactionMngrInstance.Factory.EntryDAO.Load(1);

			var lctos = Search();
			Assert.IsNotNull(lctos);
			Assert.AreEqual(1, lctos.Count);
			Assert.AreEqual(1, lctos[0].AutoId);
			Assert.AreEqual("altered", lctos[0].Memo);
		}

		[TestMethod]
		public void TestDelete()
		{
			TransactionMngrInstance.BeginTransaction();
			CreateEntry();
			TransactionMngrInstance.Commit();

			TransactionMngrInstance.BeginTransaction();
			var lcto = TransactionMngrInstance.Factory.EntryDAO.Load(1);
			TransactionMngrInstance.Factory.EntryDAO.Delete(lcto);
			TransactionMngrInstance.Commit();

			var entries = Search();
			Assert.IsNotNull(entries);
			Assert.AreEqual(0, entries.Count);
		}

		[TestMethod]
		public void TestSearchWithNoFilters()
		{
			CreateEntry();
			CreateEntry();
			CreateEntry();

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, null, null, USER, 0);
			
			Assert.IsNotNull(entries);
			Assert.AreEqual(3, entries.Count);
		}

		[TestMethod]
		public void TestSearchByDateInterval()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			for (int i = 0; i < 20; i++ )
				CreateEntry(account, center, new DateTime(2016, 12, 30).AddDays(i), i, "test");

			NHTransactionManager t = (NHTransactionManager)this.TransactionMngrInstance;
			var entries = t.Factory.EntryDAO.Search(new DateTime(2017, 1, 1), new DateTime(2017, 1, 10), null, 0, 0, 0, null, null, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(10, entries.Count);
			foreach (Entry entry in entries)
			{
				Assert.IsTrue(entry.Date >= new DateTime(2017, 1, 1));
				Assert.IsTrue(entry.Date <= new DateTime(2017, 1, 10));
			}
		}

		[TestMethod]
		public void TestSearchByExactDate()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			for (int i = 0; i < 10; i++)
				CreateEntry(account, center, new DateTime(2016, 12, 30).AddDays(i), i, "test");

			NHTransactionManager t = (NHTransactionManager)this.TransactionMngrInstance;
			var entries = t.Factory.EntryDAO.Search(null, null, new DateTime(2017, 1, 5), 0, 0, 0, null, null, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(1, entries.Count);
			Assert.IsTrue(entries[0].Date == new DateTime(2017, 1, 5));
		}

		[TestMethod]
		public void TestSearchByValueInterval()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.00M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.01M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.02M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.03M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.04M, "test");

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 1.01M, 1.03M, 0, null, null, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(3, entries.Count);
			foreach (Entry lcto in entries)
			{
				Assert.IsTrue(lcto.Value >= 1.01M);
				Assert.IsTrue(lcto.Value <= 1.03M);
			}
		}

		[TestMethod]
		public void TestSearchByExactValue()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.00M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.01M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.02M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.03M, "test");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.04M, "test");

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 1.02M, null, null, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(1, entries.Count);
			Assert.IsTrue(entries[0].Value == 1.02M);
		}

		[TestMethod]
		public void TestSearchByMemo()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.00M, "test 1");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.01M, "asdf test 2");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.02M, "asdf");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.03M, "qwer");
			CreateEntry(account, center, new DateTime(2016, 12, 30), 1.04M, "poiu");

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, null, "test", USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(2, entries.Count);
			Assert.IsTrue(entries[0].Memo.Contains("test"));
			Assert.IsTrue(entries[1].Memo.Contains("test"));
		}

		[TestMethod]
		public void TestSearchByCenter()
		{
			Account account = CreateAccount(1);
			ResultCenter center1 = CreateCenter(1);
			ResultCenter center2 = CreateCenter(2);
			CreateEntry(account, center1, new DateTime(2016, 12, 30), 1.00M, "test 1");
			CreateEntry(account, center1, new DateTime(2016, 12, 30), 1.01M, "asdf test 2");
			CreateEntry(account, center1, new DateTime(2016, 12, 30), 1.02M, "asdf");
			CreateEntry(account, center2, new DateTime(2016, 12, 30), 1.03M, "qwer");
			CreateEntry(account, center2, new DateTime(2016, 12, 30), 1.04M, "poiu");

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, center2, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(2, entries.Count);
			Assert.IsTrue(entries[0].Center.AutoId == center2.AutoId);
			Assert.IsTrue(entries[1].Center.AutoId == center2.AutoId);
		}

		[TestMethod]
		public void TestSearchByAccount()
		{
			Account account1 = CreateAccount(1, "1.01");
			Account account2 = CreateAccount(2, "1.01.01");
			Account account3 = CreateAccount(3, "1.01.02");
			Account account4 = CreateAccount(3, "1.02.01");
			Account account5 = CreateAccount(3, "2.01.01");
			ResultCenter center = CreateCenter(1);
			CreateEntry(account2, center, new DateTime(2016, 12, 30), 1.00M, "test 1");
			CreateEntry(account2, center, new DateTime(2016, 12, 30), 1.01M, "asdf test 2");
			CreateEntry(account3, center, new DateTime(2016, 12, 30), 1.02M, "asdf");
			CreateEntry(account4, center, new DateTime(2016, 12, 30), 1.03M, "qwer");
			CreateEntry(account5, center, new DateTime(2016, 12, 30), 1.04M, "poiu");

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, account1, null, null, USER, 0);

			Assert.IsNotNull(entries);
			Assert.AreEqual(3, entries.Count);
			Assert.IsTrue(entries[0].Account.Structure.StartsWith(account1.Structure));
			Assert.IsTrue(entries[1].Account.Structure.StartsWith(account1.Structure));
			Assert.IsTrue(entries[2].Account.Structure.StartsWith(account1.Structure));
		}

		[TestMethod]
		public void TestSearchByUser()
		{
			CreateEntry();
			CreateEntry();
			CreateEntry();

			var entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, null, null, USER, 2);

			Assert.IsNotNull(entries);
			Assert.AreEqual(2, entries.Count);
			Assert.AreEqual("tester", entries[0].User);
			Assert.AreEqual("tester", entries[1].User);

			entries = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, null, null, "asdf", 2);
			Assert.IsNotNull(entries);
			Assert.AreEqual(0, entries.Count);

		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestRequiredUserInSearch()
		{
			CreateEntry();
			CreateEntry();
			CreateEntry();

			var lctos = TransactionMngrInstance.Factory.EntryDAO.Search(null, null, null, 0, 0, 0, null, null, null, "", 0);
		}

		private Account CreateAccount(long id, string structure = null)
		{
			Account account = new Account();
			account.Structure = structure ?? id.ToString();
			account.Description = "Account " + id;
			account.User = USER;

			return TransactionMngrInstance.Factory.AccountDAO.Update(account);
		}

		private Account UpdateConta(DAOFactory daoFactory, Account account)
		{
			daoFactory.AccountDAO.Update(account);
			return account;
		}

		private ResultCenter CreateCenter(long id)
		{
			ResultCenter center = new ResultCenter();
			center.Code = id.ToString();
			center.Description = "Center " + id;
			center.User = USER;

			return TransactionMngrInstance.Factory.ResultCenterDAO.Update(center);
		}

		private ResultCenter UpdateCentroResultado(DAOFactory daoFactory, ResultCenter center)
		{
			daoFactory.ResultCenterDAO.Update(center);
			return center;
		}

		private void CreateEntry()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			CreateEntry(account, center, new DateTime(2017, 2, 28), 100.00M, "Test");
		}

		private void CreateEntry(Account account, ResultCenter center, 
			DateTime date, decimal value, string memo)
		{
			Entry lcto = new Entry();
			lcto.Account = account;
			lcto.Center = center;
			lcto.Date = date;
			lcto.Value = value;
			lcto.Memo = memo;
			lcto.User = USER;

			TransactionMngrInstance.Factory.EntryDAO.Update(lcto);
		}

		private IList<Entry> Search()
		{
			var entries = TransactionMngrInstance.Factory.EntryDAO
				.Search(null, null, null, 0, 0, 0, null, null, null, USER, 0);
			return entries;
		}

	}
}

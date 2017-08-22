using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class EntryTemplateDAOTest
		: BaseTest
	{
		private const string USER = "tester";

		[TestMethod]
		public void TestNewUpdate()
		{
			CreateEntryTemplate();

			var entries = TransactionMngrInstance.Factory.EntryTemplateDAO.LoadAll(USER);
			Assert.IsNotNull(entries);
			Assert.AreEqual(1, entries.Count);
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
			CreateEntryTemplate();
			CreateEntryTemplate();
			CreateEntryTemplate();

			var entries = TransactionMngrInstance.Factory.EntryTemplateDAO.LoadAll(USER);
			Assert.IsNotNull(entries);
			Assert.AreEqual(3, entries.Count);
		}

		[TestMethod]
		public void TestLoadUpdate()
		{
			CreateEntryTemplate();
			EntryTemplate beforeUpdate = TransactionMngrInstance.Factory.EntryTemplateDAO.Load(1);
			beforeUpdate.Memo = "altered";
			TransactionMngrInstance.Factory.EntryTemplateDAO.Update(beforeUpdate);
			EntryTemplate afterUpdate = TransactionMngrInstance.Factory.EntryTemplateDAO.Load(1);

			var entriestemplates = TransactionMngrInstance.Factory.EntryTemplateDAO.LoadAll(USER);
			Assert.IsNotNull(entriestemplates);
			Assert.AreEqual(1, entriestemplates.Count);
			Assert.AreEqual(1, entriestemplates[0].AutoId);
			Assert.AreEqual("altered", entriestemplates[0].Memo);
		}

		[TestMethod]
		public void TestDelete()
		{
			TransactionMngrInstance.BeginTransaction();
			CreateEntryTemplate();
			TransactionMngrInstance.Commit();

			TransactionMngrInstance.BeginTransaction();
			var entrytemplate = TransactionMngrInstance.Factory.EntryTemplateDAO.Load(1);
			TransactionMngrInstance.Factory.EntryTemplateDAO.Delete(entrytemplate);
			TransactionMngrInstance.Commit();

			var entries = TransactionMngrInstance.Factory.EntryTemplateDAO.LoadAll(USER);
			Assert.IsNotNull(entries);
			Assert.AreEqual(0, entries.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestRequiredUserInSearch()
		{
			CreateEntryTemplate();
			CreateEntryTemplate();
			CreateEntryTemplate();

			var lctos = TransactionMngrInstance.Factory.EntryTemplateDAO.LoadAll(string.Empty);
		}

		private void CreateEntryTemplate()
		{
			Account account = CreateAccount(1);
			ResultCenter center = CreateCenter(1);
			CreateEntryTemplate(account, center, 100.00M, "Test");
		}

		private Account CreateAccount(long id, string structure = null)
		{
			Account account = new Account();
			account.Structure = structure ?? id.ToString();
			account.Description = "Account " + id;
			account.User = USER;

			return TransactionMngrInstance.Factory.AccountDAO.Update(account);
		}

		private ResultCenter CreateCenter(long id)
		{
			ResultCenter center = new ResultCenter();
			center.Code = id.ToString();
			center.Description = "Center " + id;
			center.User = USER;

			return TransactionMngrInstance.Factory.ResultCenterDAO.Update(center);
		}

		private void CreateEntryTemplate(Account account, ResultCenter center, 
			decimal value, string memo)
		{
			EntryTemplate et = new EntryTemplate();
			et.Title = "Title...";
			et.Account = account;
			et.Center = center;
			et.Value = value;
			et.Memo = memo;
			et.User = USER;

			TransactionMngrInstance.Factory.EntryTemplateDAO.Update(et);
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class AccountDAOTest
		: BaseTest
	{
		private const string USER = "tester";

		[TestMethod]
		public void TestNewUpdate()
		{
			CreateAccount(1);

			var accounts = TransactionMngrInstance.Factory.AccountDAO.LoadAll(USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(1, accounts.Count);
		}

		[TestMethod]
		public void TestNewUpdateWithParentAccount()
		{
			Account account1 = new Account();
			account1.Structure = "1";
			account1.Description = "Account 1";
			account1.User = USER;

			TransactionMngrInstance.Factory.AccountDAO.Update(account1);

			Account account101 = new Account();
			account101.Structure = "1.01";
			account101.Description = "Account 1.01";
			account101.User = USER;
			account101.ParentAccount = account1;

			TransactionMngrInstance.Factory.AccountDAO.Update(account101);

			var accounts = TransactionMngrInstance.Factory.AccountDAO.LoadAll(USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(2, accounts.Count);
			Assert.IsNotNull(accounts[1].ParentAccount);
		}

		[TestMethod]
		public void TestLoadUpdate()
		{
			CreateAccount(1);
			Account accountBeforeUpdtate = TransactionMngrInstance.Factory.AccountDAO.Load(1);
			accountBeforeUpdtate.Description = "altered";
			TransactionMngrInstance.Factory.AccountDAO.Update(accountBeforeUpdtate);
			Account accountAfterUpdate = TransactionMngrInstance.Factory.AccountDAO.Load(1);

			var accounts = TransactionMngrInstance.Factory.AccountDAO.LoadAll(USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(1, accounts.Count);
			Assert.AreEqual(1, accounts[0].AutoId);
			Assert.AreEqual("altered", accounts[0].Description);
		}

		[TestMethod]
		public void TestDelete()
		{
			TransactionMngrInstance.BeginTransaction();
			CreateAccount(1);
			TransactionMngrInstance.Commit();

			TransactionMngrInstance.BeginTransaction();
			var account = TransactionMngrInstance.Factory.AccountDAO.Load(1);
			TransactionMngrInstance.Factory.AccountDAO.Delete(account);
			TransactionMngrInstance.Commit();

			var accounts = TransactionMngrInstance.Factory.AccountDAO.LoadAll(USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(0, accounts.Count);
		}

		[TestMethod]
		public void TestLoadAll()
		{
			CreateAccount(1);
			CreateAccount(2);
			CreateAccount(3);

			var accounts = TransactionMngrInstance.Factory.AccountDAO.LoadAll(USER);
			
			Assert.IsNotNull(accounts);
			Assert.AreEqual(3, accounts.Count);
		}

		[TestMethod]
		public void TestLoadFirstMatch()
		{
			//build
			CreateAccount(1);
			CreateAccount(2);
			CreateAccount(3);

			//operate
			var account = TransactionMngrInstance.Factory.AccountDAO.LoadFirstMatch("ount", USER);
			
			//check
			Assert.IsNotNull(account);
			Assert.AreEqual(1, account.AutoId);
		}

		[TestMethod]
		public void TestLoadFirstMatchWithNoMatch()
		{
			CreateAccount(1);
			CreateAccount(2);
			CreateAccount(3);

			var account = TransactionMngrInstance.Factory.AccountDAO.LoadFirstMatch("asdf", USER);
			
			Assert.IsNull(account);
		}

		[TestMethod]
		public void TestSmartSearch()
		{
			CreateConta("1", "Despesas");
			CreateConta("1.01", "Alimentacao");
			CreateConta("1.02", "Combustivel");
			CreateConta("1.03", "Moradia");
			CreateConta("2", "Receitas");
			CreateConta("2.01", "Salario");

			var accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch("1", USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(5, accounts.Count); //as quatro contas com estrutura 1 e a conta 2.01

			accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch("1.01", USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(1, accounts.Count);

			accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch("s", USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(4, accounts.Count);

			accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch("%", USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(6, accounts.Count, "It should find all the accounts when it uses wildcard char.");

			accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch("", USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(6, accounts.Count, "It should find all the accounts when it uses empty.");

			accounts = TransactionMngrInstance.Factory.AccountDAO.SmartSearch(null, USER);
			Assert.IsNotNull(accounts);
			Assert.AreEqual(6, accounts.Count, "It should find 0 account when it uses null.");

		}

		private void CreateAccount(long id)
		{
			CreateConta(id.ToString(), "Account " + id);
		}

		private void CreateConta(string structure, string description)
		{
			Account account = new Account();
			account.Structure = structure;
			account.Description = description;
			account.User = USER;

			TransactionMngrInstance.Factory.AccountDAO.Update(account);
		}

	}
}

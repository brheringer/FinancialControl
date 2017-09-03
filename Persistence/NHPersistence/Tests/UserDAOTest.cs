using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class UserDAOTest
		: BaseTest
	{
		[TestMethod]
		public void TestNewUpdateUser()
		{
			User user = CreateUserAsdf();

			User after = TransactionMngrInstance.Factory.UserDAO.Update(user);

			Assert.IsNotNull(after);
			Assert.AreEqual(1, after.AutoId);

			User reloaded = TransactionMngrInstance.Factory.UserDAO.Load(1);
			Assert.AreEqual(user.Banished, reloaded.Banished);
			Assert.AreEqual(user.BanishedReason, reloaded.BanishedReason);
			Assert.AreEqual(user.Email, reloaded.Email);
			Assert.AreEqual(user.HashedPassword, reloaded.HashedPassword);
			Assert.AreEqual(user.Name, reloaded.Name);
			Assert.AreEqual(user.Salt, reloaded.Salt);
			Assert.AreEqual(user.UserName, reloaded.UserName);
		}

		[TestMethod]
		public void TestLoadUpdateUser()
		{
			User user = CreateUserAsdf();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			User reloaded = TransactionMngrInstance.Factory.UserDAO.Load(1);
			reloaded.Banished = false;
			reloaded.BanishedReason = "";
			
			TransactionMngrInstance.Factory.UserDAO.Update(reloaded);

			User updated = TransactionMngrInstance.Factory.UserDAO.Load(1);
			Assert.AreEqual(1, updated.AutoId);
			Assert.AreEqual("asdf", updated.UserName);
			Assert.AreEqual(false, updated.Banished);
			Assert.AreEqual("", updated.BanishedReason);
		}

		[TestMethod]
		public void TestLoadByUsername()
		{
			User user = CreateUserAsdf();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			User reloaded = TransactionMngrInstance.Factory.UserDAO.LoadBy("asdf");

			User updated = TransactionMngrInstance.Factory.UserDAO.Load(1);
			Assert.AreEqual(1, reloaded.AutoId);
			Assert.AreEqual("asdf", reloaded.UserName);
		}

		[TestMethod]
		public void TestRetrieveHashedPassword()
		{
			User user = CreateUserAsdf();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			string hp = TransactionMngrInstance.Factory.UserDAO.RetrieveHashedPassword(1);
			Assert.AreEqual("123456", hp);
		}

		[TestMethod]
		public void TestRetrieveSalt()
		{
			User user = CreateUserAsdf();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			string salt = TransactionMngrInstance.Factory.UserDAO.RetrieveSalt(1);
			Assert.AreEqual("fakesalt", salt);
		}

		[TestMethod]
		[ExpectedException(typeof(InstanceNotFoundException))]
		public void TestDeleteUser()
		{
			User user = CreateUserAsdf();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			User reloaded = TransactionMngrInstance.Factory.UserDAO.Load(1);
			Assert.IsNotNull(reloaded);

			TransactionMngrInstance.Factory.UserDAO.Delete(reloaded);

			TransactionMngrInstance.Factory.UserDAO.Load(1);
		}

		[TestMethod]
		public void TestSearchUsersByPartialUserName()
		{
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserAsdf());
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserQwerty());

			var users = TransactionMngrInstance.Factory.UserDAO.Search("wer", null, null, 0, 0);
			Assert.IsNotNull(users);
			Assert.AreEqual(1, users.Count);
			Assert.AreEqual("qwerty", users[0].UserName);
		}

		[TestMethod]
		public void TestSearchUsersByPartialName()
		{
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserAsdf());
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserQwerty());

			var users = TransactionMngrInstance.Factory.UserDAO.Search(null, "roga", null, 0, 0);
			Assert.IsNotNull(users);
			Assert.AreEqual(1, users.Count);
			Assert.AreEqual("qwerty", users[0].UserName);
		}

		[TestMethod]
		public void TestSearchUsersByBanished()
		{
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserAsdf());
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserQwerty());

			var users = TransactionMngrInstance.Factory.UserDAO.Search(null, null, true, 0, 0);
			Assert.IsNotNull(users);
			Assert.AreEqual(1, users.Count);
			Assert.AreEqual("asdf", users[0].UserName);
		}

		[TestMethod]
		public void TestLimitedSearchUsers()
		{
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserAsdf());
			TransactionMngrInstance.Factory.UserDAO.Update(CreateUserQwerty());

			var users = TransactionMngrInstance.Factory.UserDAO.Search(null, null, false, 1, 0);
			Assert.IsNotNull(users);
			Assert.AreEqual(1, users.Count);
		}

		private User CreateUserAsdf()
		{
			User user = new User();
			user.Banished = true;
			user.BanishedReason = "tests";
			user.Email = "asdf@mailinator.com";
			user.HashedPassword = "123456";
			user.Name = "Astolfo Silva Diniz Ferreira";
			user.Salt = "fakesalt";
			user.UserName = "asdf";
			return user;
		}

		private User CreateUserQwerty()
		{
			User user = new User();
			user.Email = "qwerty@mailinator.com";
			user.HashedPassword = "123456";
			user.Name = "Quiroga Wellington Erlon Rosa Tavares Yield";
			user.Salt = "anotherfakesalt";
			user.UserName = "qwerty";
			return user;
		}
	}
}

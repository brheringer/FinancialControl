using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;
using FinancialControl.Persistence;
using FinancialControl.Persistence.NHPersistence;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class UserSessionDAOTest
		: BaseTest
	{
		[TestMethod]
		public void TestNewUpdateUserSession()
		{
			User user = CreateUserQwerty();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			UserSession s = new UserSession();
			s.CreationDateTime = new DateTime(2017, 3, 7, 21, 21, 21);
			s.SessionId = "FAKE_SESSION_ID";
			s.UserLoggedIn = user;

			UserSession after = TransactionMngrInstance.Factory.UserSessionDAO.Update(s);

			Assert.IsNotNull(after);
			Assert.AreEqual(1, after.AutoId);

			UserSession reloaded = TransactionMngrInstance.Factory.UserSessionDAO.Load(1);
			Assert.AreEqual(s.CreationDateTime, reloaded.CreationDateTime);
			Assert.AreEqual(s.SessionId, reloaded.SessionId);
			Assert.AreEqual(s.UserLoggedIn.UserName, reloaded.UserLoggedIn.UserName);
		}

		[TestMethod]
		public void TestLoadByUserNameSessionId()
		{
			User user = CreateUserQwerty();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			UserSession s = new UserSession();
			s.CreationDateTime = new DateTime(2017, 3, 7, 21, 21, 21);
			s.SessionId = "FAKE_SESSION_ID";
			s.UserLoggedIn = user;

			TransactionMngrInstance.Factory.UserSessionDAO.Update(s);

			UserSession loaded = TransactionMngrInstance.Factory.UserSessionDAO.LoadBy("qwerty", "FAKE_SESSION_ID");

			Assert.IsNotNull(loaded);
			Assert.AreEqual(1, loaded.AutoId);
			Assert.AreEqual("FAKE_SESSION_ID", loaded.SessionId);
			Assert.AreEqual("qwerty", loaded.UserLoggedIn.UserName);
		}

		[TestMethod]
		public void TestLoadByUserSessionId()
		{
			User user = CreateUserQwerty();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			UserSession s = new UserSession();
			s.CreationDateTime = new DateTime(2017, 3, 7, 21, 21, 21);
			s.SessionId = "FAKE_SESSION_ID";
			s.UserLoggedIn = user;

			TransactionMngrInstance.Factory.UserSessionDAO.Update(s);

			UserSession loaded = TransactionMngrInstance.Factory.UserSessionDAO.LoadBy(user, "FAKE_SESSION_ID");

			Assert.IsNotNull(loaded);
			Assert.AreEqual(1, loaded.AutoId);
			Assert.AreEqual("FAKE_SESSION_ID", loaded.SessionId);
			Assert.AreEqual("qwerty", loaded.UserLoggedIn.UserName);
		}

		[TestMethod]
		public void TestSearchSession()
		{
			User user = CreateUserQwerty();
			TransactionMngrInstance.Factory.UserDAO.Update(user);

			UserSession s = new UserSession();
			s.CreationDateTime = new DateTime(2017, 3, 7, 21, 21, 21);
			s.SessionId = "FAKE_SESSION_ID";
			s.UserLoggedIn = user;

			TransactionMngrInstance.Factory.UserSessionDAO.Update(s);

			var sessions = TransactionMngrInstance.Factory.UserSessionDAO.Search("qwerty", "FAKE_SESSION_ID");

			Assert.IsNotNull(sessions);
			Assert.AreEqual(1, sessions.Count);
			Assert.AreEqual("FAKE_SESSION_ID", sessions[0].SessionId);
			Assert.AreEqual("qwerty", sessions[0].UserLoggedIn.UserName);
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

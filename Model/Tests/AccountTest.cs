using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Model.Tests
{
	[TestClass]
	public class AccountTest
	{
		[TestMethod]
		public void TestLevel()
		{
			Assert.AreEqual(1, Account.GetLevel("1"));
			Assert.AreEqual(1, Account.GetLevel("02"));
			Assert.AreEqual(2, Account.GetLevel("03.0004"));
			Assert.AreEqual(3, Account.GetLevel("03.0004.13245"));
			Assert.AreEqual(0, Account.GetLevel(string.Empty));
			Assert.AreEqual(0, Account.GetLevel(null));
		}
	}
}

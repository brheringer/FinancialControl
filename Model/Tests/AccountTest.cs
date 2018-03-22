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

		[TestMethod]
		public void TestCompareTo()
		{
			Account account1 = new Account() { Structure = "1" };
			Account account101 = new Account() { Structure = "1.01" };
			Account account102 = new Account() { Structure = "1.02" };
			Account account102Again = new Account() { Structure = "1.02" };
			Account account2 = new Account() { Structure = "2" };
			Account accountNull = new Account() { Structure = null };
			Account accountEmpty = new Account() { Structure = string.Empty };

			Assert.AreEqual(-1, account101.CompareTo(account102), "1.01 should be before then 1.02.");
			Assert.AreEqual(1, account102.CompareTo(account101), "1.02 should be after then 1.01.");
			Assert.AreEqual(-1, account1.CompareTo(account101), "1 should be before then 1.01.");
			Assert.AreEqual(1, account2.CompareTo(account101), "2 should be after then 1.01.");
			Assert.AreEqual(0, account101.CompareTo(account101), "An object should be equals it self.");
			Assert.AreEqual(0, account102.CompareTo(account102Again), "Different instances with same struct should be equals.");
			Assert.AreEqual(1, account101.CompareTo(null), "By definition, null should be before an instance.");
			Assert.AreEqual(1, account101.CompareTo(accountNull), "By definition, null structure should be before a valid structure.");
			Assert.AreEqual(1, account101.CompareTo(accountEmpty), "By definition, empty strucutre should be before a valid structure.");
			Assert.AreEqual(-1, accountNull.CompareTo(accountEmpty), "By definition, null strucutre should be before an empty structure.");
		}

		[TestMethod]
		public void TestParentStructure()
		{
			Assert.AreEqual("1.01", new Account() { Structure = "1.01.001" }.ParentStructure);
			Assert.AreEqual("1", new Account() { Structure = "1.01" }.ParentStructure);
			Assert.AreEqual("", new Account() { Structure = "1" }.ParentStructure);
			Assert.AreEqual("", new Account() { Structure = "asdf" }.ParentStructure);
			Assert.AreEqual("", new Account() { Structure = "." }.ParentStructure);
			Assert.AreEqual("..", new Account() { Structure = "..." }.ParentStructure);
			Assert.AreEqual(null, new Account() { Structure = null }.ParentStructure);
		}
	}
}

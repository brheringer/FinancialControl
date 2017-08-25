using System;
using System.Collections.Generic;
using FinancialControl.Business.Reports;
using FinancialControl.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialControl.Business.Reports.Tests
{
	[TestClass]
	public class AccountsTotalizationsReportTest
	{
		[TestMethod]
		public void TestGenerateReportWithGivenAccounts()
		{
			var report = BuildReport(true);
			var totalizations = report.GenerateReport();

			Assert.AreEqual(11, totalizations.Count);

			AssertTotalization("1", 1123.45M, totalizations[0]);
			AssertTotalization("1.01", 423.45M, totalizations[1]);
			AssertTotalization("1.01.01", 223.45M, totalizations[2]);
			AssertTotalization("1.01.02", 200.00M, totalizations[3]);
			AssertTotalization("1.02", 700.00M, totalizations[4]);
			AssertTotalization("1.02.01", 300.00M, totalizations[5]);
			AssertTotalization("1.02.02", 400.00M, totalizations[6]);
			AssertTotalization("2", 500.00M, totalizations[7]);
			AssertTotalization("2.01", 500.00M, totalizations[8]);
			AssertTotalization("2.01.01", 500.00M, totalizations[9]);
			AssertTotalization("2.01.02", 0.00M, totalizations[10]);

			Assert.AreEqual(0, totalizations[0].Entries.Count);
			Assert.AreEqual(0, totalizations[1].Entries.Count);
			Assert.AreEqual(2, totalizations[2].Entries.Count, "1.01.01 totalization should have 2 entries.");
		}

		[TestMethod]
		public void TestGenerateReportWithGivenOutAccounts()
		{
			var report = BuildReport(false);
			var totalizations = report.GenerateReport();

			Assert.AreEqual(10, totalizations.Count);
		}

		private AccountsTotalizatonsReport BuildReport(bool withGivenAccounts)
		{
			Account a1 = new Account() { Structure = "1" };
			Account a101 = new Account() { Structure = "1.01", ParentAccount = a1 };
			Account a10101 = new Account() { Structure = "1.01.01", ParentAccount = a101 };
			Account a10102 = new Account() { Structure = "1.01.02", ParentAccount = a101 };
			Account a102 = new Account() { Structure = "1.02", ParentAccount = a1 };
			Account a10201 = new Account() { Structure = "1.02.01", ParentAccount = a102 };
			Account a10202 = new Account() { Structure = "1.02.02", ParentAccount = a102 };
			Account a2 = new Account() { Structure = "2" };
			Account a201 = new Account() { Structure = "2.01", ParentAccount = a2 };
			Account a20101 = new Account() { Structure = "2.01.01", ParentAccount = a201 };
			Account a20102 = new Account() { Structure = "2.01.02", ParentAccount = a201 };

			Entry entry10101a = new Entry() { Account = a10101, Value = 123.45M };
			Entry entry10101b = new Entry() { Account = a10101, Value = 100.00M };
			Entry entry10102 = new Entry() { Account = a10102, Value = 200.00M };
			Entry entry10201 = new Entry() { Account = a10201, Value = 300.00M };
			Entry entry10202 = new Entry() { Account = a10202, Value = 400.00M };
			Entry entry20101 = new Entry() { Account = a20101, Value = 500.00M };

			var report = new AccountsTotalizatonsReport();
			if(withGivenAccounts)
				report.AddAccounts(new List<Account>() { a1, a101, a10101, a10102, a102, a10201, a10202, a2, a201, a20101, a20102 });
			report.AddEntries(new List<Entry>() { entry10101a, entry10101b, entry10102, entry10201, entry10202, entry20101 });
			return report;
		}

		private void AssertTotalization(string expectedStructure, decimal expectedTotal, AccountTotalization totalization)
		{
			Assert.AreEqual(expectedStructure, totalization.Account.Structure);
			Assert.AreEqual(expectedTotal, totalization.Total);
		}
	}
}

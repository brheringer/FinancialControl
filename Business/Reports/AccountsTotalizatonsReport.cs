using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Business.Reports
{
	public class AccountsTotalizatonsReport
	{
		private List<Account> accounts = new List<Account>();
		private List<Entry> entries = new List<Entry>();
		private List<AccountTotalization> totalizations = new List<AccountTotalization>();

		public void AddAccounts(IEnumerable<Account> accounts)
		{
			this.accounts.AddRange(accounts);
		}

		public void AddEntries(IEnumerable<Entry> entries)
		{
			this.entries.AddRange(entries);
		}

		public List<AccountTotalization> GenerateReport()
		{
			InitializeTotalizationWithAccounts();
			TotalizeEntries();

			this.totalizations.Sort();
			return this.totalizations;
		}

		private void InitializeTotalizationWithAccounts()
		{
			//this is usefull if it is desired to show accounts without entries.
			foreach (Account account in this.accounts)
				this.totalizations.Add(new AccountTotalization() { Account = account });
		}

		private void TotalizeEntries()
		{
			foreach (Entry e in this.entries)
			{
				var analyticalTotalization = Totalize(e.Account, e.Value);
				analyticalTotalization.Entries.Add(e);
			}
		}

		private AccountTotalization Totalize(Account account, decimal value)
		{
			AccountTotalization totalization = new AccountTotalization();
			totalization.Account = account;

			int i = totalizations.IndexOf(totalization);
			if (i >= 0)
				totalization = this.totalizations[i];
			else
				this.totalizations.Add(totalization);

			totalization.Total += value;

			if (account.ParentAccount != null)
				Totalize(account.ParentAccount, value);

			return totalization;
		}
	}
}

using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Business.Reports
{
	public class AccountTotalization : IComparable<AccountTotalization>
	{
		public Account Account { get; set; }
		public decimal Total { get; set; }
		public List<Entry> Entries { get; set; }

		public AccountTotalization()
		{
			this.Entries = new List<Entry>();
		}

		public override bool Equals(object obj)
		{
			if (obj == this) return true;
			if (obj == null) return false;
			if (obj.GetType() != this.GetType()) return false;
			AccountTotalization other = (AccountTotalization)obj;
			return object.Equals(this.Account, other.Account);
		}

		public override int GetHashCode()
		{
			return this.Account == null ? 0 : this.Account.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0} = {1:C}", this.Account, this.Total);
		}

		public int CompareTo(AccountTotalization other)
		{
			if (other == null) return 1;
			if (this.Account == null && other.Account == null) return 0;
			if (this.Account == null && other.Account != null) return -1;
			return this.Account.CompareTo(other.Account);
		}
	}
}
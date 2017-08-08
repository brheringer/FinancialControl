using System;

namespace FinancialControl.Model
{
	public class EntryTemplate : Entity
	{
		public virtual ResultCenter Center { get; set; }

		public virtual Account Account { get; set; }

		public virtual string Memo { get; set; }

		public virtual decimal Value { get; set; }

		public override void Validate()
		{
			ValidateAccount();
			ValidateCenter();
			ValidateMemo();
			ValidateValue();
		}

		private void ValidateAccount()
		{
		}

		private void ValidateCenter()
		{
		}

		private void ValidateMemo()
		{
		}

		private void ValidateValue()
		{
		}

		public override string ToString()
		{
			return string.Format("{0} @ {1} : {2}", 
				this.Value, this.Account, this.Center);
		}
	}
}

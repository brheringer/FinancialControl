using System;

namespace FinancialControl.Model
{
	public class Entry : Entity
	{
		public virtual ResultCenter Center { get; set; }

		public virtual Account Account { get; set; }

		public virtual DateTime Date { get; set; }

		public virtual string Memo { get; set; }

		public virtual decimal Value { get; set; }

		public override void Validate()
		{
			ValidateAccount();
			ValidateCenter();
			ValidateDate();
			ValidateMemo();
			ValidateValue();
		}

		private void ValidateAccount()
		{
			ValidateRequiredProperty(() => this.Account);
		}

		private void ValidateCenter()
		{
			ValidateRequiredProperty(() => this.Center);
		}

		private void ValidateDate()
		{
			ValidateRequiredProperty(() => this.Date);
		}

		private void ValidateMemo()
		{
			ValidateRequiredProperty(() => this.Memo);
		}

		private void ValidateValue()
		{
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", 
				this.Date.ToShortDateString(), 
				this.Value, this.Account);
		}
	}
}

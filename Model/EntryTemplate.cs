using System;

namespace FinancialControl.Model
{
	public class EntryTemplate : Entity
	{
		public virtual string Title { get; set; }

		public virtual ResultCenter Center { get; set; }

		public virtual Account Account { get; set; }

		public virtual string Memo { get; set; }

		public virtual decimal Value { get; set; }

		public override void Validate()
		{
			ValidateTitle();
			ValidateAccount();
			ValidateCenter();
			ValidateMemo();
			ValidateValue();
		}

		private void ValidateTitle()
		{
			ValidateRequiredProperty(() => this.Title);
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
			return this.Title;
		}
	}
}

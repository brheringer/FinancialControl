using System;

namespace FinancialControl.Model
{
	public class MemoMapping : Entity
	{
		public virtual string IncomingMemo { get; set; }

		public virtual ResultCenter MappedCenter { get; set; }

		public virtual Account MappedAccount { get; set; }

		public virtual string MappedMemo { get; set; }

		public override void Validate()
		{
			ValidateIncomingMemo();
			ValidateMappedAccount();
			ValidateMappedCenter();
			ValidateMappedMemo();
		}

		private void ValidateIncomingMemo()
		{
			ValidateRequiredProperty(() => this.IncomingMemo);
		}

		private void ValidateMappedAccount()
		{
		}

		private void ValidateMappedCenter()
		{
		}

		private void ValidateMappedMemo()
		{
		}

		public override string ToString()
		{
			return IncomingMemo;
		}
	}
}

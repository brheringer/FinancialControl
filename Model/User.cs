using System;
using System.Collections.Generic;

namespace FinancialControl.Model
{
	public class User : Entity
	{
		public virtual string Name { get; set; }
		public virtual string UserName { get; set; }
		public virtual string HashedPassword { get; set; }
		public virtual string Salt { get; set; }
		public virtual bool Banished { get; set; }
		public virtual string BanishedReason { get; set; }
		public virtual string Email { get; set; }

		public override void Validate()
		{
			ValidateName();
			ValidateUserName();
			ValidateHasedPassword();
			ValidateSalt();
			ValidateEmail();
			ValidateBanished();
			ValidateBanishedReason();
		}

		private void ValidateName()
		{
			ValidateRequiredProperty(() => this.Name, 255);
		}

		private void ValidateUserName()
		{
			ValidateRequiredProperty(() => this.UserName, 16);
		}

		private void ValidateHasedPassword()
		{
			ValidateRequiredProperty(() => this.HashedPassword, 255);
		}

		private void ValidateSalt()
		{
			ValidateRequiredProperty(() => this.Salt, 255);
		}

		private void ValidateBanished()
		{
		}

		private void ValidateBanishedReason()
		{
			if (this.Banished)
			{
				ValidateRequiredProperty(() => this.BanishedReason);
			}
		}

		private void ValidateEmail()
		{
			ValidatePropertyValue(() => this.Email, 255);
			//ver antes de querer implementar uma regex:
			//http://stackoverflow.com/questions/201323/using-a-regular-expression-to-validate-an-email-address
		}

		public override bool Equals(object obj)
		{
			return EntityHelper.EqualsByReferenceByType(obj, this)
				?? string.Equals(this.UserName, ((User)obj).UserName);
		}

		public override int GetHashCode()
		{
			return EntityHelper.GetHashCode(this.UserName);
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}

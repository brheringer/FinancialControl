using System;

namespace FinancialControl.Model
{
	public class ResultCenter : Entity
	{
		public virtual string Code { get; set; }

		public virtual string Description { get; set; }

		public override void Validate()
		{
			ValidateCode();
			ValidateDescription();
		}

		private void ValidateCode()
		{
			ValidateRequiredProperty(() => this.Code);
			ValidatePropertyValue(() => this.Description, 255);
		}

		private void ValidateDescription()
		{
			ValidateRequiredProperty(() => this.Description);
			ValidatePropertyValue(() => this.Description, 255);
		}

		public override string ToString()
		{
			return this.Description;
		}
	}
}

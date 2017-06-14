using System;

namespace FinancialControl.Model
{
	public class Account : Entity
	{
		private const char LEVEL_SEPARATOR = '.';

		public virtual Account ParentAccount { get; set; }

		public virtual string Description { get; set; }

		public virtual string Structure { get; set; }

		public virtual int Level
		{
			get
			{
				return GetLevel(this.Structure);
			}
		}

		public virtual EntryType Type { get; set; }

		public override void Validate()
		{
			ValidateParentAccount();
			ValidateDescription();
			ValidateStructure();
			ValidateType();
		}

		private void ValidateParentAccount()
		{
		}

		private void ValidateDescription()
		{
			ValidateRequiredProperty(() => this.Description);
			ValidatePropertyValue(() => this.Description, 255);
		}

		private void ValidateStructure()
		{
			ValidateRequiredProperty(() => this.Structure);
			ValidatePropertyValue(() => this.Structure, 255);
		}

		private void ValidateType()
		{
		}

		public override string ToString()
		{
			return String.Format("{0} {1}", this.Structure, this.Description);
		}

		public static int GetLevel(string estrutura) {
			int nivel = 0;
			if (!string.IsNullOrEmpty(estrutura))
			{
				nivel = estrutura.Split(LEVEL_SEPARATOR).Length;
			}
			return nivel;
		}
	}
}

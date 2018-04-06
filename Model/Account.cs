using System;

namespace FinancialControl.Model
{
	public class Account : Entity, IComparable<Account>
	{
		private const char LEVEL_SEPARATOR = '.';

		public virtual Account ParentAccount { get; set; }

		public virtual string Description { get; set; }

		public virtual string Structure { get; set; }

		public virtual string ParentStructure
		{
			get
			{
				if (this.Structure == null)
					return null;
				int i = this.Structure.LastIndexOf(LEVEL_SEPARATOR);
				return i >= 0
					? this.Structure.Substring(0, i)
					: string.Empty;
			}
		}

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

		public override bool Equals(object obj)
		{
			if (obj == this) return true;
			if (obj == null) return false;
			if (obj.GetType() != this.GetType()) return false;
			Account other = (Account)obj;
			return this.CompareTo(other) == 0;
		}

		public override int GetHashCode()
		{
			return this.Structure == null ? 0 : this.Structure.GetHashCode();
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

		public virtual int CompareTo(Account other)
		{
			if (other == null) return 1;
			if (this.Structure == null && other.Structure == null) return 0;
			if (this.Structure == null && other.Structure != null) return -1;
			return this.Structure.CompareTo(other.Structure);
		}
	}
}

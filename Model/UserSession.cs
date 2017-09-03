using System;

namespace FinancialControl.Model
{
	public class UserSession : Entity
	{
		public virtual User UserLoggedIn { get; set; }
		public virtual string SessionId { get; set; }
		public virtual DateTime CreationDateTime { get; set; }

		public override bool Equals(object obj)
		{
			return EntityHelper.EqualsByReferenceByType(obj, this)
				?? string.Equals(this.SessionId, ((UserSession)obj).SessionId);
		}

		public override int GetHashCode()
		{
			return this.SessionId.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{1}: {0}", this.SessionId, this.UserLoggedIn);
		}

		public override void Validate()
		{
		}

		public virtual bool TimeOut(int limitInSeconds)
		{
			TimeSpan ts = (DateTime.Now - this.CreationDateTime);
			return ts.Seconds > limitInSeconds;
		}
	}
}

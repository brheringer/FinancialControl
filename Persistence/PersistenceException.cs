using System;

namespace FinancialControl.Persistence
{
	public class PersistenceException
		: Exception
	{
		public PersistenceException() : this(string.Empty) { }
		public PersistenceException(string message) : base(message) { }
		public PersistenceException(string message, Exception inner) : base(message, inner) { }
	}
}

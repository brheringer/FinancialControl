using System;

namespace FinancialControl.Model
{
	public class ModelException : Exception
	{
		public ModelException() { }
		public ModelException(string message) : base(message) { }
		public ModelException(string message, Exception inner) : base(message, inner) { }
	}
}

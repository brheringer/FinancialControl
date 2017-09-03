using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.Model
{
	public class PropertyModelException
		: ModelException
	{
		public string Entity { get; set; }

		public string Property { get; set; }

		protected PropertyModelException(string msg, string entity, string property)
			: this(msg, entity, property, null)
		{
		}

		protected PropertyModelException(string msg, string entity, string property, Exception innerException)
			: base(msg, innerException)
		{
			this.Entity = entity;
			this.Property = property;
		}

	}
}

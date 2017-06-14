using System;

namespace FinancialControl.DataTransferObjects
{
	public class EntityDto 
		: DataTransferObject
	{
		public long AutoId { get; set; }
		public string Presentation { get; set; }
		public string User { get; set; }
		public int Version { get; set; }
	}
}
using System;

namespace FinancialControl.DataTransferObjects
{
	public class AccountDto
		: EntityDto
	{
		public string Description { get; set; }
		public string Structure { get; set; }
		public string Type { get; set; }
		public int Level { get; set; }
		public EntityReferenceDto ParentAccount { get; set; }
	}
}
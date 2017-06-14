using System;

namespace FinancialControl.DataTransferObjects
{
	public class UserDto 
		: EntityDto
	{
		public string Name { get; set; }
		public string UserName { get; set; }
		public string NewPassword { get; set; }
		public string OldPassword { get; set; }
		public bool Banished { get; set; }
		public string BanishedReason { get; set; }
		public string Email { get; set; }
	}
}

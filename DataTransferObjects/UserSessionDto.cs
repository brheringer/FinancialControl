using System;

namespace FinancialControl.DataTransferObjects
{
	public class UserSessionDto 
		: EntityDto
	{
		public string UserSessionId { get; set; }
		public string UserName { get; set; }
		public bool IsAdmin { get; set; }

		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(UserSessionId)
					&& !string.IsNullOrEmpty(UserName);
			}
		}

		public UserSessionDto()
		{
			this.IsAdmin = false;
		}
	}
}

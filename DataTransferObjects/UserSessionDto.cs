using System;

namespace FinancialControl.DataTransferObjects
{
	public class UserSessionDto 
		: EntityDto
	{
		public string UserSessionToken { get; set; }
		public string UserName { get; set; }
		public bool IsAdmin { get; set; }

		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(UserSessionToken)
					&& !string.IsNullOrEmpty(UserName);
			}
		}

		public UserSessionDto()
		{
			this.IsAdmin = false;
		}
	}
}

using System;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class UserSessionWrapper
	{
		public static UserSessionDto Wrap(UserSession session)
		{
			UserSessionDto dto = new UserSessionDto();
			EntityWrapper.WrapIntoDto(session, dto);
			dto.UserSessionId = session.SessionId;
			dto.UserName = session.UserLoggedIn.UserName;
			return dto;
		}
	}
}
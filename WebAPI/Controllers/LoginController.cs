using System;
using System.Web.Http;
using FinancialControl.DataTransferObjects;
using FinancialControl.Persistence;
using FinancialControl.Security;

namespace FinancialControl.WebAPI.Controllers
{
    public class LoginController : TransactionalApiContoller
    {
		[HttpPost]
		public UserSessionDto Login(UserDto dto)
		{
			return InvokeCommandInsideTransactionAnoymously(daoFactory => Login(daoFactory, dto));
		}

		private UserSessionDto Login(DaoFactory daoFactory, UserDto login)
		{
			if (login == null)
				login = new UserDto();

			//LOADS
			var user = daoFactory.UserDAO.LoadBy(login.UserName);

			//DELEGATE BUSINESS
			LoginEngine engine = new LoginEngine();

			var session = engine.DoLogin(user, login.NewPassword);

			//PERSIST
			daoFactory.UserSessionDAO.Update(session);

			//DELIVER
			return Wrappers.UserSessionWrapper.Wrap(session); ;
		}

		[HttpPost]
		[Route("api/login/register")]
		public UserDto Register(UserDto dto)
		{
			return InvokeCommandInsideTransactionAnoymously(daoFactory => Register(daoFactory, dto));
		}

		private UserDto Register(DaoFactory daoFactory, UserDto dto)
		{
			if (dto == null)
				dto = new UserDto();

			FinancialControl.Model.User user = new FinancialControl.Model.User();
			user.Name = dto.UserName;
			user.UserName = dto.UserName;
			user.User = dto.UserName;
			//user.Email;

			LoginEngine engine = new LoginEngine();
			engine.SetPasswordBySideEffect(user, dto.NewPassword);

			daoFactory.UserDAO.Update(user);

			return new UserDto() { UserName = dto.UserName };
		}
	}
}

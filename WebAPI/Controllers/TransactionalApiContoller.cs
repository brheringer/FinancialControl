using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FinancialControl.DataTransferObjects;
using FinancialControl.Persistence;

namespace FinancialControl.WebAPI.Controllers
{
	public abstract class TransactionalApiContoller
		: ApiController
	{
		//Controller é uma instância por requisição
		private FinancialControl.Model.UserSession UserSessionProxy { get; set; }

		protected string UserName 
		{ 
			get {
				return this.UserSessionProxy != null
					&& this.UserSessionProxy.UserLoggedIn != null
					? this.UserSessionProxy.UserLoggedIn.UserName
					: string.Empty;
			} 
		}

		protected ResultType InvokeCommandInsideTransactionAnoymously<ResultType>(Func<DaoFactory, ResultType> command)
			where ResultType : class, ResponseEnvelop, new()
		{
			using (TransactionManager tx = GlobalContext.Instance().TransactionManagerFactory.Create())
			{
				return tx.InvokeCommandInsideTransactionAnonymously(command);
			}
		}

		protected ResultType InvokeCommandInsideTransaction<ResultType>(Func<DaoFactory, ResultType> command)
			where ResultType : class, ResponseEnvelop, new()
		{
			try
			{
				using (TransactionManager tx = GlobalContext.Instance().TransactionManagerFactory.Create())
				{
					SetUserSessionInfoToRequestContext();
					return tx.InvokeCommandInsideTransaction(command, this.UserSessionProxy);
				}
			}
			catch(Exception ex)
			{
				var dto = new ResultType();
				dto.Response.Exception = ex.ToString();
				return dto;
			}
		}

		private void SetUserSessionInfoToRequestContext()
		{
			const string USER_NAME_HEADER = "UserName";
			const string USER_SESSION_ID_HEADER = "SessionId";

			this.UserSessionProxy = new FinancialControl.Model.UserSession();
			this.UserSessionProxy.SessionId = RetrieveUserSessionHeader(USER_SESSION_ID_HEADER);
			this.UserSessionProxy.UserLoggedIn = new FinancialControl.Model.User();
			this.UserSessionProxy.UserLoggedIn.UserName = RetrieveUserSessionHeader(USER_NAME_HEADER);
		}

		private string RetrieveUserSessionHeader(string headerName)
		{
			IEnumerable<string> values = Request.Headers.GetValues(headerName);
			return values.FirstOrDefault();
		}

		protected void UserSiege(string username)
		{
			if (!string.Equals(username, this.UserName, StringComparison.InvariantCultureIgnoreCase))
				throw new Exception(string.Format("User conflict: {0} {1}", username, this.UserName));
		}
	}
}
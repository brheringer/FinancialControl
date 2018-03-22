using System;
using System.Collections.Generic;
using System.Linq;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public abstract class TransactionManagerBase
		: TransactionManager
	{
		protected bool TransactionIsOpen;

		public DAOFactory Factory { get; set; }

		public ResultType InvokeCommandInsideTransaction<ResultType>(Func<DAOFactory, ResultType> command, UserSession sessionProxy)
			where ResultType : class//, ResponseEnvelop
		{
			return InvokeCommandInsideTransaction(command, false, sessionProxy);
		}

		public ResultType InvokeCommandInsideTransactionAnonymously<ResultType>(Func<DAOFactory, ResultType> command)
			where ResultType : class//, ResponseEnvelop
		{
			return InvokeCommandInsideTransaction(command, true, null);
		}

		private ResultType InvokeCommandInsideTransaction<ResultType>(Func<DAOFactory, ResultType> command, bool anonymous, UserSession sessionProxy)
			where ResultType : class//, ResponseEnvelop
		{
			try
			{
				BeginTransaction();
				if (!anonymous)
					ValidateUserSession(this.Factory, sessionProxy);
				ResultType resultado = command.Invoke(this.Factory);
				Commit();
				//IncludeWarningsInResult(resultado);
				return resultado;
			}
			catch//(Exception ex)
			{
				if (this.TransactionIsOpen)
				{
					//TODO log and answer
					Rollback();
				}

				throw;
				//BusinessException bex;
				//if (ex is BusinessException)
				//{
				//	bex = (BusinessException)ex;
				//}
				//else
				//{
				//	bex = new BusinessException(ex.Message, ex);
				//	Context.Contextualizer.CurrentRequestContext.Logger.Log(ex);
				//}

				//if (Contextualizer.CurrentRequestContext.IsRunningLocally)
				//{
				//	//local: rethrow
				//	throw;
				//}
				//else
				//{
				//	//remote: send response with exception data
				//	Type type = typeof(ResultType);
				//	ResponseEnvelop instance = (ResponseEnvelop)Activator.CreateInstance(type, true);
				//	if (instance != null)
				//	{
				//		instance.Response.AddBusinessException(bex);
				//	}
				//	return (ResultType)instance;
				//}
			}
		}

		public virtual void BeginTransaction()
		{
			TransactionIsOpen = true;
			return;
		}

		public virtual void Commit()
		{
			TransactionIsOpen = false;
			return;
		}

		public virtual void Rollback()
		{
			TransactionIsOpen = false;
			return;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected bool IsDisposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing) return;
			if (!IsDisposed && TransactionIsOpen)
			{
				Rollback();
			}
			Factory = null;
			IsDisposed = true;
		}

		//private void IncludeWarningsInResult(ResponseEnvelop result)
		//{
		//	if (result != null)
		//	{
		//		var notifier = Contextualizer.CurrentRequestContext.Notifier;
		//		if (notifier.HasWarnings)
		//		{
		//			result.Response.AddWarnings(notifier.ExtractWarnings());
		//		}
		//	}
		//}

		private void ValidateUserSession(DAOFactory daoFactory, UserSession sessionInfo)
		{
			if (sessionInfo == null)
			{
				throw new Exception("Invalid session. Try to login again.");
			}

			try
			{
				UserSession userSession = daoFactory.UserSessionDAO.LoadBy(sessionInfo.UserLoggedIn, sessionInfo.SessionId);

				int limitInSeconds = 4 * 60 * 60; //TODO parametrizar
				//TODO ta verificando o tempo desde a criacao da sessao, mas talvez deveria verificar desde a ultima atividade
				if (userSession.TimeOut(limitInSeconds))
				{
					throw new Exception("Session expired. Try to login again.");
				}

				//TODO verificar se o usuario foi bloqueado (durante a sessao dele)
				//TODO verificar permissoes?
			}
			catch (InstanceNotFoundException ex)
			{
				//TODO essa validação tinha que ser em Business
				//mas aí essa camada ia ter que conhecer Business, o que também seria ruim
				throw new Exception("Invalid session.", ex);
			}
		}

	}
}

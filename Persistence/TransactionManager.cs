﻿using System;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface TransactionManager
		: IDisposable
	{
		void BeginTransaction();

		void Commit();

		void Rollback();

		ResultType InvokeCommandInsideTransactionAnonymously<ResultType>(Func<DAOFactory, ResultType> command)
			where ResultType : class;//, ResponseEnvelop;

		ResultType InvokeCommandInsideTransaction<ResultType>(Func<DAOFactory, ResultType> command, UserSession sessionProxy)
			where ResultType : class;//, ResponseEnvelop;
		//TODO duvida: nao podia usar a interface direto ao inves de "ResultType : ResponseEnvelop"?
	}
}

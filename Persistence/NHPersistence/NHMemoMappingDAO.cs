using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHMemoMappingDAO
		: NHibernateDAO<MemoMapping>, MemoMappingDAO
	{
		public NHMemoMappingDAO(ISession session)
			: base(session)
		{
		}

		public IList<MemoMapping> LoadAll(string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<MemoMapping>();
			FilterByUser(query, username);
			return query.List();
		}
	}
}

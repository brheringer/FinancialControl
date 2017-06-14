using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHResultCenterDAO
		: NHibernateDAO<ResultCenter>, ResultCenterDAO
	{
		public NHResultCenterDAO(ISession session)
			: base(session)
		{
		}

		public ResultCenter LoadFirstMatch(string description, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException("Usuário é obrigatório.");

			var query = this.SessionInstance.QueryOver<ResultCenter>();
			FilterByUser(query, username);
			query.WhereRestrictionOn(x => x.Description).IsLike(description, MatchMode.Anywhere);
			query.Take(1);
			IList<ResultCenter> centros = query.List();
			return centros.Count > 0
				? centros[0]
				: null;
		}

		public IList<ResultCenter> LoadAll(string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException("Usuário é obrigatório.");

			var query = this.SessionInstance.QueryOver<ResultCenter>();
			FilterByUser(query, username);
			query.OrderBy(x => x.Description);
			return query.List();
		}

		public IList<ResultCenter> Search(string codigo, string descricao, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException("Usuário é obrigatório.");

			var query = this.SessionInstance.QueryOver<ResultCenter>();
			FilterByUser(query, username);

			if (FilterHelper.IsFilterUsed(codigo))
				query.WhereRestrictionOn(x => x.Code).IsLike(codigo, MatchMode.Anywhere);
			
			if (FilterHelper.IsFilterUsed(descricao))
				query.WhereRestrictionOn(x => x.Description).IsLike(descricao, MatchMode.Anywhere);
			
			query.OrderBy(x => x.Description);
			
			return query.List();
		}

		public IList<ResultCenter> SmartSearch(string smartEntry, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException("Usuário é obrigatório.");

			var query = this.SessionInstance.QueryOver<ResultCenter>();
			FilterByUser(query, username);

			query.WhereRestrictionOn(x => x.Description).IsLike(smartEntry, MatchMode.Anywhere);

			query.OrderBy(x => x.Description);

			return query.List();
		}
	}
}

using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHAccountDAO 
		: NHibernateDAO<Account>, AccountDAO
	{
		public NHAccountDAO(ISession session)
			: base(session)
		{
		}

		public Account LoadFirstMatch(string description, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<Account>();
			FilterByUser(query, username);
			query.WhereRestrictionOn(x => x.Description).IsLike(description, MatchMode.Anywhere);
			query.Take(1);
			IList<Account> accounts = query.List();
			return accounts.Count > 0
				? accounts[0]
				: null;
		}

		public IList<Account> LoadAll(string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<Account>();
			FilterByUser(query, username);
			query.OrderBy(x => x.Structure);
			return query.List();
		}

		public IList<Account> Search(string description, string structure, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<Account>();
			FilterByUser(query, username);
			query.WhereRestrictionOn(x => x.Description).IsLike(description, MatchMode.Anywhere);
			query.WhereRestrictionOn(x => x.Structure).IsLike(structure, MatchMode.Start);
			IList<Account> accounts = query.List();
			return accounts;
		}

		public IList<Account> SmartSearch(string smartEntry, string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<Account>();
			FilterByUser(query, username);

			query.And(Restrictions.On<Account>(x => x.Structure).IsLike(smartEntry, MatchMode.Anywhere)
				|| Restrictions.On<Account>(x => x.Description).IsLike(smartEntry, MatchMode.Anywhere));

			query.OrderBy(x => x.Description);

			return query.List();
		}
	}
}

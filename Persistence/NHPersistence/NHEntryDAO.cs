using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHEntryDAO
		: NHibernateDAO<Entry>, EntryDAO
	{
		public NHEntryDAO(ISession session)
			: base(session)
		{
		}

		public IList<Entry> Search(
			DateTime? initialDate, DateTime? finalDate, DateTime? exactDate,
			decimal lowerValue, decimal higherValue, decimal exactValue,
			Account account, ResultCenter center, string memo, string username,
			int searchLimit)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<Entry>();

			if(FilterHelper.IsFilterUsed(initialDate))
				query.Where(x => x.Date >= initialDate.Value.Date);

			if (FilterHelper.IsFilterUsed(finalDate))
				query.Where(x => x.Date <= finalDate.Value.Date);

			if (FilterHelper.IsFilterUsed(exactDate))
				query.Where(x => x.Date == exactDate.Value.Date);

			if (FilterHelper.IsFilterUsed(lowerValue))
				query.Where(x => x.Value >= lowerValue);

			if (FilterHelper.IsFilterUsed(higherValue))
				query.Where(x => x.Value <= higherValue);

			if (FilterHelper.IsFilterUsed(exactValue))
				query.Where(x => x.Value == exactValue);

			if (FilterHelper.IsFilterUsed(account)) 
			{
				query.JoinQueryOver(x => x.Account)
					.WhereRestrictionOn(c => c.Structure)
					.IsLike(account.Structure, MatchMode.Start);
			}

			if (FilterHelper.IsFilterUsed(center))
				query.Where(x => x.Center == center);

			if (FilterHelper.IsFilterUsed(memo))
				query.WhereRestrictionOn(x => x.Memo).IsLike(memo, MatchMode.Anywhere);

			FilterByUser(query, username);

			query.OrderBy(x => x.Date);

			if (searchLimit > 0)
				query.Take(searchLimit);

			return query.List();
		}

	}
}

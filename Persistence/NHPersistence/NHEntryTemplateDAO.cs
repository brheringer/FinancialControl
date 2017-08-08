using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHEntryTemplateDAO
		: NHibernateDAO<EntryTemplate>, EntryTemplateDAO
	{
		public NHEntryTemplateDAO(ISession session)
			: base(session)
		{
		}

		public IList<EntryTemplate> LoadAll(string username)
		{
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException();

			var query = this.SessionInstance.QueryOver<EntryTemplate>();
			FilterByUser(query, username);
			return query.List();
		}
	}
}

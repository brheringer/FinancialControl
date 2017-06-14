using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHUserDAO 
		: NHibernateDAO<User>, UserDAO
	{
		public NHUserDAO(ISession session)
			: base(session)
		{
		}

		public override User LoadBy(string entryKey)
		{
			IQueryOver<User, User> query = this.SessionInstance.QueryOver<User>();
			query.WhereRestrictionOn(obj => obj.UserName).IsLike(entryKey, MatchMode.Exact);
			return LoadByHelper(query, entryKey);
		}

		public IList<User> Search(
			string filterUserName,
			string filterName,
			bool? filterBanished, int maxResults, int page)
		{
			IQueryOver<User, User> query = this.SessionInstance.QueryOver<User>();

			if (FilterHelper.IsFilterUsed(filterUserName))
			{
				query.WhereRestrictionOn(obj => obj.UserName).IsLike(filterUserName, MatchMode.Anywhere);
			}

			if (FilterHelper.IsFilterUsed(filterName))
			{
				query.WhereRestrictionOn(obj => obj.Name).IsLike(filterName, MatchMode.Anywhere);
			}

			if (FilterHelper.IsFilterUsed(filterBanished))
			{
				query.Where(obj => obj.Banished == filterBanished.Value);
			}

			FilterHelper.ApplyCommonFilters(query, maxResults, page);

			query.OrderBy(obj => obj.UserName).Asc();

			return query.List();
		}

		public string RetrieveHashedPassword(long userId)
		{
			string hashedPassword;
			string salt;
			RetrieveHashedPasswordAndSalt(userId, out hashedPassword, out salt);
			return hashedPassword;
		}

		public string RetrieveSalt(long userId)
		{
			string hashedPassword;
			string salt;
			RetrieveHashedPasswordAndSalt(userId, out hashedPassword, out salt);
			return salt;
		}

		private void RetrieveHashedPasswordAndSalt(long userId, out string hashedPassword, out string salt)
		{
			//TODO tem 4 hardcodes: 1 nome de entidade, 3 nomes de propriedades
			hashedPassword = string.Empty;
			salt = string.Empty;
			IQuery query = this.SessionInstance.CreateQuery("select HashedPassword, Salt from User where Autoid=:userId");
			query.SetInt64("userId", userId);
			var row = query.Enumerable();
			foreach (object[] data in row)
			{
				hashedPassword = (string)data[0];
				salt = (string)data[1];
			}
		}

	}
}

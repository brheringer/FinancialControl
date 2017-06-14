using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHUserSessionDAO 
		: NHibernateDAO<UserSession>, UserSessionDAO
	{
		public NHUserSessionDAO(ISession session)
			: base(session)
		{
		}

		public UserSession LoadBy(User user, string sessionid)
		{
			if (user == null)
				throw new ArgumentNullException();

			return LoadBy(user.UserName, sessionid);
		}

		public UserSession LoadBy(string userName, string sessionid)
		{
			var searchResult = Search(userName, sessionid);
			if (searchResult.Count == 1)
			{
				return searchResult[0];
			}
			else if (searchResult.Count > 1)
			{
				throw new TooManyInstancesFoundException(typeof(UserSession), userName + sessionid);
				//teoricamente isso não acontece
			}
			else
			{
				throw new InstanceNotFoundException(typeof(UserSession), userName + sessionid);
			}
		}

		public IList<UserSession> Search(
			string filterUserName = null,
			string filterSessionId = null)
		{
			IQueryOver<UserSession, UserSession> query = this.SessionInstance.QueryOver<UserSession>();

			if (FilterHelper.IsFilterUsed(filterUserName))
			{
				query.JoinQueryOver(x => x.UserLoggedIn)
					.WhereRestrictionOn(obj => obj.UserName)
					.IsLike(filterUserName, MatchMode.Exact);
			}

			if (FilterHelper.IsFilterUsed(filterSessionId))
			{
				query.WhereRestrictionOn(obj => obj.SessionId).IsLike(filterSessionId, MatchMode.Exact);
			}

			//FilterHelper.ApplyCommonFilters(query, 0, 0); //TODO

			//query.OrderBy(obj => obj.UserName).Asc();

			return query.List();
		}
	}
}

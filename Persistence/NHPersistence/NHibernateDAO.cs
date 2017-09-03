using System;
using System.Collections.Generic;
using System.Linq;
using FinancialControl.Model;
using NHibernate;
using NHibernate.Criterion;

namespace FinancialControl.Persistence.NHPersistence
{
	public abstract class NHibernateDAO<EntityType>
		: Dao<EntityType>
		where EntityType : Entity
	{
		public readonly ISession SessionInstance;

		protected NHibernateDAO(ISession session)
		{
			this.SessionInstance = session;
		}

		public virtual void Delete(EntityType entity)
		{
			this.SessionInstance.Delete(entity);
		}

		public virtual EntityType LoadBy(string entryKey)
		{
			long id;
			long.TryParse(entryKey, out id);
			return Load(id);
		}

		public virtual EntityType Load(long id)
		{
			EntityType entidade = this.SessionInstance.Get<EntityType>(id);
			if (entidade == null)
			{
				throw new InstanceNotFoundException(typeof(EntityType), id);
			}
			return entidade;
		}

		public virtual EntityType Update(EntityType entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			//if (!entity.IsPersistent)
			//{
			//	//entity.CreationDateTime = DateTime.Now;
			//	//entity.CreatorUser; //TODO ?
			//	//entity.LastUpdateDateTime = entity.CreationDateTime;
			//	//entity.LastUpdateDateTime = entity.CreatorUser; //TODO ?
			//}
			//else
			//{
			//	//entity.LastUpdateDateTime = DateTime.Now;
			//	//entity.LastUpdateDateTime = entity.CreatorUser; //TODO ?
			//}

			this.SessionInstance.SaveOrUpdate(entity);
			return entity;
		}

		protected EntityType LoadByHelper(IQueryOver<EntityType> query, string entryKey)
		{
			var list = query.List();
			if (list.Count == 1)
			{
				return list[0];
			}
			else if (list.Count < 1)
			{
				throw new InstanceNotFoundException(typeof(EntityType), entryKey);
			}
			else
			{
				throw new TooManyInstancesFoundException(typeof(EntityType), entryKey);
			}
		}

		protected void FilterByUser(IQueryOver<EntityType, EntityType> query, string username)
		{
			//TODO problema: username com _ ou % (wildcard)
			query.WhereRestrictionOn(x => x.User).IsInsensitiveLike(username, MatchMode.Exact);
		}
	}
}

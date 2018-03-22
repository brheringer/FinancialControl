using System;
using FinancialControl.Model;

namespace FinancialControl.Persistence
{
	public interface Dao<EntityType>
		where EntityType : Entity
	{
		void Delete(EntityType entity);

		EntityType ResolveProxy(EntityType proxy);

		EntityType Load(long id);

		EntityType LoadBy(string entryKey);

		EntityType Update(EntityType entity);
	}
}

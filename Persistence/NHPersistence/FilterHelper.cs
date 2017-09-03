using System;
using System.Collections.Generic;
using NHibernate;

namespace FinancialControl.Persistence.NHPersistence
{
	public static class FilterHelper
	{
		public static void ApplyCommonFilters<EntityType>(IQueryOver<EntityType, EntityType> queryOver, int maxResults, int page)
		{
			if (maxResults > 0)
			{
				queryOver.Take(maxResults);

				if (page > 0)
				{
					queryOver.Skip(page * maxResults);
				}
			}
		}

		public static bool IsFilterUsed(string filter)
		{
			return !string.IsNullOrEmpty(filter);
		}

		public static bool IsFilterUsed(DateTime filter)
		{
			return filter > DateTime.MinValue;
		}

		public static bool IsFilterUsed(decimal filter)
		{
			return filter > 0;
		}

		public static bool IsFilterUsed(object filter)
		{
			return filter != null;
		}
	}
}

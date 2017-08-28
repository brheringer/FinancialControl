using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialControl.Model;
using FinancialControl.Persistence;
using FinancialControl.DataTransferObjects;
using FinancialControl.WebAPI.Wrappers;

namespace FinancialControl.WebAPI.Controllers
{
	public class EntryController 
		: TransactionalApiContoller
    {
		public EntryDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private EntryDto Get(DaoFactory daoFactory, int id)
		{
			Entry entry = daoFactory.EntryDAO.Load(id);
			UserSiege(entry.User);
			return EntryWrapper.Wrap(entry);
		}

		[HttpPost]
		[Route("api/entry/search")]
		public EntriesDto Search(EntriesDto filtros)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, filtros));
		}

		private EntriesDto Search(DaoFactory daoFactory, EntriesDto filters)
		{
			Account accountFilter = null;
			if (filters.FilterAccount != null && filters.FilterAccount.AutoId > 0)
				accountFilter = daoFactory.AccountDAO.Load(filters.FilterAccount.AutoId);

			ResultCenter filterCenter = null;
			if (filters.FilterCenter != null && filters.FilterCenter.AutoId > 0)
				filterCenter = daoFactory.ResultCenterDAO.Load(filters.FilterCenter.AutoId);

			IList<Entry> entries = daoFactory.EntryDAO.Search(
				filters.FilterInitialDate, filters.FilterFinalDate, filters.FilterExactDate,
				filters.FilterLowerValue, filters.FilterHigherSuperior, filters.FilterExactValue,
				accountFilter, filterCenter, 
				filters.FilterMemo, 
				this.UserName, 
				filters.SearchMaxResults);
			
			return EntryWrapper.Wrap(entries);
		}

		public EntryDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private EntryDto Delete(DaoFactory daoFactory, int id)
		{
			Entry entry = daoFactory.EntryDAO.Load(id);
			UserSiege(entry.User);
			daoFactory.EntryDAO.Delete(entry);
			return new EntryDto();
		}

		[HttpPost]
		[Route("api/entry/update")]
		public EntryDto Update(EntryDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private EntryDto Update(DaoFactory daoFactory, EntryDto dto)
		{
			Entry entry = EntryWrapper.Wrap(dto);

			if (entry.IsPersistent)
				UserSiege(entry.User);
			else
				entry.User = this.UserName;

			entry.Validate();
			entry = daoFactory.EntryDAO.Update(entry);
			return EntryWrapper.Wrap(entry);
		}
    }
}

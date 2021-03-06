﻿using System;
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

		private EntryDto Get(DAOFactory daoFactory, int id)
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

		private EntriesDto Search(DAOFactory daoFactory, EntriesDto filters)
		{
			Account accountFilter = null;
			if (filters.FilterAccount != null && filters.FilterAccount.AutoId > 0)
				accountFilter = daoFactory.AccountDAO.Load(filters.FilterAccount.AutoId);

			ResultCenter filterCenter = null;
			if (filters.FilterCenter != null && filters.FilterCenter.AutoId > 0)
				filterCenter = daoFactory.ResultCenterDAO.Load(filters.FilterCenter.AutoId);

			IList<Entry> entries = daoFactory.EntryDAO.Search(
				filters.FilterInitialDate, filters.FilterFinalDate, filters.FilterExactDate,
				filters.FilterLowerValue, filters.FilterHigherValue, filters.FilterExactValue,
				accountFilter, filterCenter, 
				filters.FilterMemo, 
				this.UserName, 
				filters.SearchMaxResults,
				filters.SearchPage);
			
			return EntryWrapper.Wrap(entries);
		}

		[HttpDelete]
		[Route("api/entry")]
		public EntryDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private EntryDto Delete(DAOFactory daoFactory, int id)
		{
			Entry entry = daoFactory.EntryDAO.Load(id);
			UserSiege(entry.User);
			daoFactory.EntryDAO.Delete(entry);
			return new EntryDto();
		}

		[HttpPost]
		[Route("api/entry")]
		public EntryDto Update(EntryDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private EntryDto Update(DAOFactory daoFactory, EntryDto dto)
		{
			Entry entry;

			if (dto.AutoId > 0)
			{
				entry = daoFactory.EntryDAO.Load(dto.AutoId);
				UserSiege(entry.User);
			}
			else
			{
				dto.User = this.UserName;
				entry = new Entry();
			}

			EntryWrapper.WrapInto(dto, entry);

			entry.Account = daoFactory.AccountDAO.ResolveProxy(entry.Account);
			entry.Center = daoFactory.ResultCenterDAO.ResolveProxy(entry.Center);

			entry.Validate();
			entry = daoFactory.EntryDAO.Update(entry);
			return EntryWrapper.Wrap(entry);
		}

	}
}

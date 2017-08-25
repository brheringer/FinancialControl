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
	public class AccountController : TransactionalApiContoller
    {
		public AccountDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private AccountDto Get(DaoFactory daoFactory, int id)
		{
			Account account = daoFactory.AccountDAO.Load(id);
			UserSiege(account.User);
			return AccountWrapper.Wrap(account);
		}

		public AccountsDto Get()
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory));
		}

		private AccountsDto Get(DaoFactory daoFactory)
		{
			AccountsDto dto = new AccountsDto();
			IList<Account> accounts = daoFactory.AccountDAO.LoadAll(this.UserName);
			dto.Accounts = AccountWrapper.Wrap(accounts);
			return dto;
		}

		public AccountDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private AccountDto Delete(DaoFactory daoFactory, int id)
		{
			Account account = daoFactory.AccountDAO.Load(id);
			UserSiege(account.User);
			daoFactory.AccountDAO.Delete(account);
			return new AccountDto();
		}

		[HttpPost]
		[Route("api/account/update")]
		public AccountDto Update(AccountDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private AccountDto Update(DaoFactory daoFactory, AccountDto dto)
		{
			Account account = AccountWrapper.Wrap(dto);

			if (account.IsPersistent)
				UserSiege(account.User);
			else
				account.User = this.UserName;

			account.Validate();
			account = daoFactory.AccountDAO.Update(account);

			return AccountWrapper.Wrap(account);
		}

		[HttpPost]
		[Route("api/account/search")]
		public AccountsDto Search(AccountsDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, dto));
		}

		private AccountsDto Search(DaoFactory daoFactory, AccountsDto dto)
		{
			AccountsDto response = new AccountsDto();
			var accounts = daoFactory.AccountDAO.Search(dto.FilterStructure, dto.FilterDescription, this.UserName);
			response.Accounts = AccountWrapper.Wrap(accounts);
			return response;
		}

		[HttpGet]
		[Route("api/account/smartSearch/{smartEntry}")]
		public EntitiesReferencesDto SmartSearch(string smartEntry)
		{
			return InvokeCommandInsideTransaction(daoFactory => SmartSearch(daoFactory, smartEntry));
		}

		private EntitiesReferencesDto SmartSearch(DaoFactory daoFactory, string smartEntry)
		{
			EntitiesReferencesDto response = new EntitiesReferencesDto();
			var accounts = daoFactory.AccountDAO.SmartSearch(smartEntry, this.UserName);
			response = Wrappers.EntityWrapper.WrapToReferences(accounts);
			return response;
		}
	}
}

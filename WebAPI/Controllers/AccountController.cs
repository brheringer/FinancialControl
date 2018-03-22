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
	public class AccountController : TransactionalApiContoller
    {
		public AccountDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private AccountDto Get(DAOFactory daoFactory, int id)
		{
			Account account = daoFactory.AccountDAO.Load(id);
			UserSiege(account.User);
			return AccountWrapper.Wrap(account);
		}

		[HttpGet]
		[Route("api/account/all")]
		public EntitiesReferencesDto GetAll()
		{
			return InvokeCommandInsideTransaction(daoFactory => GetAll(daoFactory));
		}

		private EntitiesReferencesDto GetAll(DAOFactory daoFactory)
		{
			IList<Account> accounts = daoFactory.AccountDAO.LoadAll(this.UserName);
			return EntityWrapper.WrapToReferences<Account>(accounts);
		}

		public AccountDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private AccountDto Delete(DAOFactory daoFactory, int id)
		{
			Account account = daoFactory.AccountDAO.Load(id);
			UserSiege(account.User);
			daoFactory.AccountDAO.Delete(account);
			return new AccountDto();
		}

		[HttpPost]
		public AccountDto Update(AccountDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private AccountDto Update(DAOFactory daoFactory, AccountDto dto)
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

		private AccountsDto Search(DAOFactory daoFactory, AccountsDto dto)
		{
			AccountsDto response = new AccountsDto();
			var accounts = daoFactory.AccountDAO.Search(dto.FilterDescription, dto.FilterStructure, this.UserName);
			response.Accounts = AccountWrapper.Wrap(accounts);
			return response;
		}

		[HttpPost]
		[Route("api/account/smartSearch")]
		public EntitiesReferencesDto SmartSearch([FromBody]SmartEntryDto smartEntry)
		{
			return InvokeCommandInsideTransaction(daoFactory => SmartSearch(daoFactory, smartEntry.SmartEntry));
		}

		private EntitiesReferencesDto SmartSearch(DAOFactory daoFactory, string smartEntry)
		{
			EntitiesReferencesDto response = new EntitiesReferencesDto();
			var accounts = daoFactory.AccountDAO.SmartSearch(smartEntry, this.UserName);
			response = Wrappers.EntityWrapper.WrapToReferences(accounts);
			return response;
		}
	}
}

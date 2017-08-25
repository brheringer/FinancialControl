using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class AccountWrapper
	{
		public static Account Wrap(AccountDto dto)
		{
			Account account = new Account();
			EntityWrapper.WrapIntoEntity(dto, account);
			account.Structure = dto.Structure;
			account.Description = dto.Description;
			account.ParentAccount = EntityWrapper.CreateProxy<Account>(dto.ParentAccount);
			account.Type = (Model.EntryType)Enum.Parse(typeof(Model.EntryType), dto.Type);
			return account;
		}

		public static IList<AccountDto> Wrap(IList<Account> accounts)
		{
			List<AccountDto> wrapped = new List<AccountDto>();
			foreach (Account c in accounts)
				wrapped.Add(Wrap(c));
			return wrapped;
		}

		public static AccountDto Wrap(Account account)
		{
			AccountDto dto = new AccountDto();
			EntityWrapper.WrapIntoDto(account, dto);
			dto.Structure = account.Structure;
			dto.Description = account.Description;
			dto.ParentAccount = EntityWrapper.WrapToReference(account.ParentAccount);
			dto.Level = account.Level;
			dto.Type = account.Type.ToString();
			return dto;
		}
	}
}
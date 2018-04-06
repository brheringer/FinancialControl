using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialControl.Business.Reports;
using FinancialControl.Model;
using FinancialControl.Persistence;
using FinancialControl.DataTransferObjects;
using FinancialControl.WebAPI.Wrappers;

namespace FinancialControl.WebAPI.Controllers
{
	public class AccountsTotalizationsReportController
		: TransactionalApiContoller
	{
		[HttpPost]
		public AccountsTotalizationsReportDto GenerateReport(AccountsTotalizationsReportDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, dto));
		}

		private AccountsTotalizationsReportDto Search(DAOFactory daoFactory, AccountsTotalizationsReportDto dto)
		{
			AccountsTotalizationsReportDto response = new AccountsTotalizationsReportDto();

			var accounts = daoFactory.AccountDAO.LoadAll(this.UserName);
			var entries = daoFactory.EntryDAO.Search(dto.FilterInitialDate, dto.FilterFinalDate,
				null, 0, 0, 0, null, null, null, this.UserName, 0, 0);

			AccountsTotalizatonsReport report = new AccountsTotalizatonsReport();
			report.AddAccounts(accounts);
			report.AddEntries(entries);
			var totalizations = report.GenerateReport();

			response.AccountsTotalizations = Wrap(totalizations);
			return response;
		}

		private IList<AccountTotalizationDto> Wrap(List<AccountTotalization> totalizations)
		{
			IList<AccountTotalizationDto> lista = new List<AccountTotalizationDto>();
			foreach (var t in totalizations)
			{
				var dto = new AccountTotalizationDto();
				dto.AccountStructure = t.Account.Structure;
				dto.AccountDescription = t.Account.Description;
				dto.Total = t.Total;
				//TODO dto.CenterStructure;
				//TODO dto.CenterDescription;
				dto.AccountLevel = t.Account.Level;
				dto.IsDetail = t.Entries.Count > 0 && t.Entries[0].Account == t.Account;
				dto.AnalyticalDetails = new List<AccountTotalizationDto.DetailDto>();
				foreach (var entry in t.Entries)
				{
					var detail = new AccountTotalizationDto.DetailDto();
					detail.Date = entry.Date;
					detail.Value = entry.Value;
					detail.Memo = entry.Memo;
					detail.OriginalEntry = Wrappers.EntityWrapper.WrapToReference(entry);
					dto.AnalyticalDetails.Add(detail);
				}
				lista.Add(dto);
			}
			return lista;
		}
	}
}

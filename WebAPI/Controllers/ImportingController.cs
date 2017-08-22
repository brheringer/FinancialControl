using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialControl.Model;
using FinancialControl.Persistence;
using FinancialControl.DataTransferObjects;
using FinancialControl.WebAPI.Wrappers;
using FinancialControl.Business.Importers;

namespace FinancialControl.WebAPI.Controllers
{
	public class ImportingController
		: TransactionalApiContoller
    {
		[HttpPost]
		public ImportingDto Import()
		{
			string data = this.Request.Content.ReadAsStringAsync().Result;
			return InvokeCommandInsideTransaction(daoFactory => Import(daoFactory, data));
		}

		private ImportingDto Import(DaoFactory daoFactory, string data)
		{
			var mappings = daoFactory.MemoMappingDAO.LoadAll(this.UserName);

			FileImporter importer = new BancoBrasilCsvImporter(); //TODO fabrica
			importer.CurrentUser = this.UserName;
			importer.FileContent = data;
			importer.MemoMapper = new MemoMapperBuilder().Build(mappings);
			importer.TempAccount = daoFactory.AccountDAO.LoadFirstMatch("Importing", this.UserName); //TODO let the user choose
			importer.TempCenter = daoFactory.ResultCenterDAO.LoadFirstMatch("Importing", this.UserName); //TODO let the user choose
			importer.Import();
			foreach(Entry entry in importer.GeneratedEntries)
			{
				entry.Validate();
				daoFactory.EntryDAO.Update(entry);
			}

			var dto = new ImportingDto();
			dto.Success = true;
			dto.QuantityOfImportedEntries = importer.GeneratedEntries.Count;
			dto.TimeStamp = DateTime.Now;
			//dto.ErrorsMessages;
			return dto;
		}
    }
}

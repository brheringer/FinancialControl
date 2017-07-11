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
		public ImportingDto Import(string data)
		{
			return InvokeCommandInsideTransaction(daoFactory => Import(daoFactory, data));
		}

		private ImportingDto Import(DaoFactory daoFactory, string data)
		{
			FileImporter importer = new BancoBrasilCsvImporter(); //TODO fabrica
			importer.CurrentUser = this.UserName;
			importer.FileContent = data;
			//importer.MemoMapper = new MemoMapper(); //TODO carregar do banco os mapeamentos
			//importer.TempAccount; //TODO carregar do banco
			//importer.TempCenter; //TODO carregar do banco
			importer.Import();
			foreach(Entry entry in importer.GeneratedEntries)
			{
				entry.Validate();
				daoFactory.EntryDAO.Update(entry);
			}

			return new ImportingDto(); //EntryWrapper.Wrap(entry);
		}
    }
}

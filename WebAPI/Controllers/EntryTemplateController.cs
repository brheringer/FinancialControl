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
	public class EntryTemplateController
		: TransactionalApiContoller
    {
		public EntryTemplateDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private EntryTemplateDto Get(DAOFactory daoFactory, int id)
		{
			EntryTemplate template = daoFactory.EntryTemplateDAO.Load(id);
			UserSiege(template.User);
			return EntryTemplateWrapper.Wrap(template);
		}

		[HttpPost]
		[Route("api/entryTemplate/search")]
		public EntriesTemplatesDto Search(EntriesTemplatesDto filtros)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, filtros));
		}

		private EntriesTemplatesDto Search(DAOFactory daoFactory, EntriesTemplatesDto filters)
		{
			IList<EntryTemplate> templates = daoFactory.EntryTemplateDAO.LoadAll(this.UserName);
			
			return EntryTemplateWrapper.Wrap(templates);
		}

		public EntryTemplateDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private EntryTemplateDto Delete(DAOFactory daoFactory, int id)
		{
			EntryTemplate template = daoFactory.EntryTemplateDAO.Load(id);
			UserSiege(template.User);
			daoFactory.EntryTemplateDAO.Delete(template);
			return new EntryTemplateDto();
		}

		[HttpPost]
		public EntryTemplateDto Update(EntryTemplateDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private EntryTemplateDto Update(DAOFactory daoFactory, EntryTemplateDto dto)
		{
			EntryTemplate template = EntryTemplateWrapper.Wrap(dto);

			if (template.IsPersistent)
				UserSiege(template.User);
			else
				template.User = this.UserName;

			template.Validate();
			template = daoFactory.EntryTemplateDAO.Update(template);
			return EntryTemplateWrapper.Wrap(template);
		}
    }
}

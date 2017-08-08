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
	public class MemoMappingController
		: TransactionalApiContoller
    {
		public MemoMappingDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private MemoMappingDto Get(DaoFactory daoFactory, int id)
		{
			var mapping = daoFactory.MemoMappingDAO.Load(id);
			UserSiege(mapping.User);
			return MemoMappingWrapper.Wrap(mapping);
		}

		[HttpPost]
		[Route("api/memoMapping/search")]
		public MemosMappingsDto Search(MemosMappingsDto filtros)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, filtros));
		}

		private MemosMappingsDto Search(DaoFactory daoFactory, MemosMappingsDto filters)
		{
			IList<MemoMapping> mappings = daoFactory.MemoMappingDAO.LoadAll(this.UserName);
			
			return MemoMappingWrapper.Wrap(mappings);
		}

		public void Delete(int id)
		{
			InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private void Delete(DaoFactory daoFactory, int id)
		{
			var mapping = daoFactory.MemoMappingDAO.Load(id);
			UserSiege(mapping.User);
			daoFactory.MemoMappingDAO.Delete(mapping);
		}

		[HttpPost]
		[Route("api/memoMapping/update")]
		public MemoMappingDto Update(MemoMappingDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private MemoMappingDto Update(DaoFactory daoFactory, MemoMappingDto dto)
		{
			MemoMapping mapping = MemoMappingWrapper.Wrap(dto);

			if (mapping.IsPersistent)
				UserSiege(mapping.User);
			else
				mapping.User = this.UserName;

			mapping.Validate();
			mapping = daoFactory.MemoMappingDAO.Update(mapping);
			return MemoMappingWrapper.Wrap(mapping);
		}
    }
}

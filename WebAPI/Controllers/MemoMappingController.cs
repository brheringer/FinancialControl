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

		private MemoMappingDto Get(DAOFactory daoFactory, int id)
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

		private MemosMappingsDto Search(DAOFactory daoFactory, MemosMappingsDto filters)
		{
			IList<MemoMapping> mappings = daoFactory.MemoMappingDAO.LoadAll(this.UserName);
			
			return MemoMappingWrapper.Wrap(mappings);
		}

		public MemoMappingDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private MemoMappingDto Delete(DAOFactory daoFactory, int id)
		{
			var mapping = daoFactory.MemoMappingDAO.Load(id);
			UserSiege(mapping.User);
			daoFactory.MemoMappingDAO.Delete(mapping);
			return new MemoMappingDto();
		}

		[HttpPost]
		public MemoMappingDto Update(MemoMappingDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private MemoMappingDto Update(DAOFactory daoFactory, MemoMappingDto dto)
		{
			MemoMapping mapping = MemoMappingWrapper.Wrap(dto);

			if (mapping.IsPersistent)
				UserSiege(mapping.User);
			else
				mapping.User = this.UserName;

			mapping.MappedAccount = SolveProxy(daoFactory, mapping.MappedAccount);
			mapping.MappedCenter = SolveProxy(daoFactory, mapping.MappedCenter);
			mapping.Validate();
			mapping = daoFactory.MemoMappingDAO.Update(mapping);
			return MemoMappingWrapper.Wrap(mapping);
		}

		private Account SolveProxy(DAOFactory daoFactory, Account proxy)
		{
			return proxy != null && proxy.AutoId > 0
				? daoFactory.AccountDAO.Load(proxy.AutoId)
				: null;
		}

		private ResultCenter SolveProxy(DAOFactory daoFactory, ResultCenter proxy)
		{
			return proxy != null && proxy.AutoId > 0
				? daoFactory.ResultCenterDAO.Load(proxy.AutoId)
				: null;
		}
	}
}

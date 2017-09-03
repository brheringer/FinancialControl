using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialControl.Model;
using FinancialControl.Persistence;
using FinancialControl.DataTransferObjects;
using FinancialControl.WebAPI.Wrappers;

namespace FinancialControl.WebAPI.Controllers
{
    public class ResultCenterController 
		: TransactionalApiContoller
    {
		public ResultCenterDto Get(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id));
		}

		private ResultCenterDto Get(DaoFactory daoFactory, int id)
		{
			ResultCenter center = daoFactory.ResultCenterDAO.Load(id);
			UserSiege(center.User);
			return ResultCenterWrapper.Wrap(center);
		}

		public ResultsCentersDto Get()
		{
			return InvokeCommandInsideTransaction(daoFactory => Get(daoFactory));
		}

		private ResultsCentersDto Get(DaoFactory daoFactory)
		{
			IList<ResultCenter> centros = daoFactory.ResultCenterDAO.LoadAll(this.UserName);
			ResultsCentersDto dto = new ResultsCentersDto();
			dto.ResultsCenters = ResultCenterWrapper.Wrap(centros);
			return dto;
		}

		public ResultCenterDto Delete(int id)
		{
			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));
		}

		private ResultCenterDto Delete(DaoFactory daoFactory, int id)
		{
			ResultCenter center = daoFactory.ResultCenterDAO.Load(id);
			UserSiege(center.User);
			daoFactory.ResultCenterDAO.Delete(center);
			return new ResultCenterDto();
		}

		[HttpPost]
		[Route("api/resultCenter/update")]
		public ResultCenterDto Update(ResultCenterDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto));
		}

		private ResultCenterDto Update(DaoFactory daoFactory, ResultCenterDto dto)
		{
			ResultCenter center = ResultCenterWrapper.Wrap(dto);

			if (center.IsPersistent)
				UserSiege(center.User);
			else
				center.User = this.UserName;

			center.Validate();
			center = daoFactory.ResultCenterDAO.Update(center);

			return ResultCenterWrapper.Wrap(center);
		}

		[HttpPost]
		[Route("api/resultCenter/search")]
		public ResultsCentersDto Search(ResultCenterDto dto)
		{
			return InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, dto));
		}

		private ResultsCentersDto Search(DaoFactory daoFactory, ResultCenterDto dto)
		{
			ResultsCentersDto resposta = new ResultsCentersDto();
			var centros = daoFactory.ResultCenterDAO.Search(dto.Code, dto.Description, this.UserName);
			resposta.ResultsCenters = ResultCenterWrapper.Wrap(centros);
			return resposta;
		}

		[HttpGet]
		[Route("api/resultCenter/smartSearch/{smartEntry}")]
		public EntitiesReferencesDto SmartSearch(string smartEntry)
		{
			return InvokeCommandInsideTransaction(daoFactory => SmartSearch(daoFactory, smartEntry));
		}

		private EntitiesReferencesDto SmartSearch(DaoFactory daoFactory, string smartEntry)
		{
			EntitiesReferencesDto resposta = new EntitiesReferencesDto();
			var centros = daoFactory.ResultCenterDAO.SmartSearch(smartEntry, this.UserName);
			resposta = EntityWrapper.WrapToReferences(centros);
			return resposta;
		}
	}
}

using System;
using System.Collections.Generic;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class ResultCenterWrapper
	{
		public static ResultCenter Wrap(ResultCenterDto dto)
		{
			ResultCenter center = new ResultCenter();
			EntityWrapper.WrapIntoEntity(dto, center);
			center.Code = dto.Code;
			center.Description = dto.Description;
			return center;
		}

		public static IList<ResultCenterDto> Wrap(IList<ResultCenter> centers)
		{
			List<ResultCenterDto> wrapped = new List<ResultCenterDto>();
			foreach (ResultCenter c in centers)
				wrapped.Add(Wrap(c));
			return wrapped;
		}

		public static ResultCenterDto Wrap(ResultCenter center)
		{
			ResultCenterDto dto = new ResultCenterDto();
			EntityWrapper.WrapIntoDto(center, dto);
			dto.Code = center.Code;
			dto.Description = center.Description;
			return dto;
		}
	}
}
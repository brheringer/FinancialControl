using System;
using System.Collections.Generic;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class MemoMappingWrapper
	{
		public static MemoMapping Wrap(MemoMappingDto dto)
		{
			MemoMapping mapping = new MemoMapping();
			EntityWrapper.WrapIntoEntity(dto, mapping);
			mapping.IncomingMemo = dto.IncomingMemo;
			mapping.MappedMemo = dto.MappedMemo;
			mapping.MappedAccount = EntityWrapper.CreateProxy<Account>(dto.MappedAccount);
			mapping.MappedCenter = EntityWrapper.CreateProxy<ResultCenter>(dto.MappedCenter);
			return mapping;
		}

		public static MemosMappingsDto Wrap(IList<MemoMapping> memos)
		{
			MemosMappingsDto dto = new MemosMappingsDto();
			dto.Mappings = new List<MemoMappingDto>();
			foreach (MemoMapping m in memos)
				dto.Mappings.Add(Wrap(m));
			return dto;
		}

		public static MemoMappingDto Wrap(MemoMapping memo)
		{
			MemoMappingDto dto = new MemoMappingDto();
			EntityWrapper.WrapIntoDto(memo, dto);
			dto.IncomingMemo = memo.IncomingMemo;
			dto.MappedMemo = memo.MappedMemo;
			dto.MappedAccount = EntityWrapper.WrapToReference(memo.MappedAccount);
			dto.MappedCenter = EntityWrapper.WrapToReference(memo.MappedCenter);
			return dto;
		}
	}
}
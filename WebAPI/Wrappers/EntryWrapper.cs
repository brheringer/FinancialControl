using System;
using System.Collections.Generic;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class EntryWrapper
	{
		public static Entry Wrap(EntryDto dto)
		{
			Entry e = new Entry();
			WrapInto(dto, e);
			return e;
		}

		public static void WrapInto(EntryDto fromDto, Entry intoEntity)
		{
			EntityWrapper.WrapIntoEntity(fromDto, intoEntity);
			if(fromDto.Date != null)
				intoEntity.Date = fromDto.Date.Value;
			intoEntity.Value = fromDto.Value;
			intoEntity.Memo = fromDto.Memo;
			intoEntity.Account = EntityWrapper.CreateProxy<Account>(fromDto.Account);
			intoEntity.Center = EntityWrapper.CreateProxy<ResultCenter>(fromDto.Center);
		}

		public static EntriesDto Wrap(IList<Entry> entries)
		{
			EntriesDto dto = new EntriesDto();
			dto.Entries = new List<EntryDto>();
			foreach (Entry c in entries)
				dto.Entries.Add(Wrap(c));
			return dto;
		}

		public static EntryDto Wrap(Entry entry)
		{
			EntryDto dto = new EntryDto();
			EntityWrapper.WrapIntoDto(entry, dto);
			dto.Date = entry.Date;
			dto.Value = entry.Value;
			dto.Memo = entry.Memo;
			dto.Account = EntityWrapper.WrapToReference(entry.Account);
			dto.Center = EntityWrapper.WrapToReference(entry.Center);
			return dto;
		}
	}
}
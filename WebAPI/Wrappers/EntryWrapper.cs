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
			Entry entry = new Entry();
			EntityWrapper.WrapIntoEntity(dto, entry);
			if(dto.Date != null)
				entry.Date = dto.Date.Value;
			entry.Value = dto.Value;
			entry.Memo = dto.Memo;
			entry.Account = EntityWrapper.CreateProxy<Account>(dto.Account);
			entry.Center = EntityWrapper.CreateProxy<ResultCenter>(dto.Center);
			return entry;
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
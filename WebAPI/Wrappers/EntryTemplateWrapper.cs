using System;
using System.Collections.Generic;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class EntryTemplateWrapper
	{
		public static EntryTemplate Wrap(EntryTemplateDto dto)
		{
			EntryTemplate template = new EntryTemplate();
			WrapInto(dto, template);
			return template;
		}

		public static void WrapInto(EntryTemplateDto fromDto, EntryTemplate intoEntity)
		{
			EntityWrapper.WrapIntoEntity(fromDto, intoEntity);
			intoEntity.Title = fromDto.Title;
			intoEntity.Value = fromDto.Value;
			intoEntity.Memo = fromDto.Memo;
			intoEntity.Account = EntityWrapper.CreateProxy<Account>(fromDto.Account);
			intoEntity.Center = EntityWrapper.CreateProxy<ResultCenter>(fromDto.Center);
		}

		public static EntriesTemplatesDto Wrap(IList<EntryTemplate> templates)
		{
			EntriesTemplatesDto dto = new EntriesTemplatesDto();
			dto.Templates = new List<EntryTemplateDto>();
			foreach (EntryTemplate t in templates)
				dto.Templates.Add(Wrap(t));
			return dto;
		}

		public static EntryTemplateDto Wrap(EntryTemplate template)
		{
			EntryTemplateDto dto = new EntryTemplateDto();
			EntityWrapper.WrapIntoDto(template, dto);
			dto.Title = template.Title;
			dto.Value = template.Value;
			dto.Memo = template.Memo;
			dto.Account = EntityWrapper.WrapToReference(template.Account);
			dto.Center = EntityWrapper.WrapToReference(template.Center);
			return dto;
		}
	}
}
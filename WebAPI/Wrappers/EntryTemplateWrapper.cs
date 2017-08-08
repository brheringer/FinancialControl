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
			EntityWrapper.WrapIntoEntidade(dto, template);
			template.Value = dto.Value;
			template.Memo = dto.Memo;
			template.Account = EntityWrapper.CreateProxy<Account>(dto.Account);
			template.Center = EntityWrapper.CreateProxy<ResultCenter>(dto.Center);
			return template;
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
			dto.Value = template.Value;
			dto.Memo = template.Memo;
			dto.Account = EntityWrapper.WrapToReferencia(template.Account);
			dto.Center = EntityWrapper.WrapToReferencia(template.Center);
			return dto;
		}
	}
}
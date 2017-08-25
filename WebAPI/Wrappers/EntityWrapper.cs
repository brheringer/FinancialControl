using System;
using System.Collections.Generic;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.WebAPI.Wrappers
{
	public static class EntityWrapper
	{
		public static void WrapIntoDto(Entity entity, EntityDto dto)
		{
			if (entity == null || dto == null)
				throw new ArgumentNullException();
			dto.AutoId = entity.AutoId;
			dto.Presentation = entity.ToString();
			dto.User = entity.User;
			dto.Version = entity.Version;
		}

		public static void WrapIntoEntity(EntityDto dto, Entity entity)
		{
			if (entity == null || dto == null)
				throw new ArgumentNullException();
			entity.AutoId = dto.AutoId;
			entity.User = dto.User;
			entity.Version = dto.Version;
		}

		public static EntitiesReferencesDto WrapToReferences<T>(IEnumerable<T> entities)
			where T : Entity
		{
			EntitiesReferencesDto references = new EntitiesReferencesDto();
			foreach (T e in entities)
			{
				references.References.Add(WrapToReference(e));
			}
			return references;
		}

		public static EntityReferenceDto WrapToReference(Entity entidade)
		{
			EntityReferenceDto dto = new EntityReferenceDto();
			if (entidade != null)
			{
				dto.AutoId = entidade.AutoId;
				dto.Presentation = entidade.ToString();
			}
			return dto;
		}

		public static T CreateProxy<T>(EntityReferenceDto dto)
			where T : Entity, new()
		{
			if (dto == null || dto.AutoId == 0)
				return null;

			T entity = new T();
			entity.AutoId = dto.AutoId;
			return entity;
		}
	}
}
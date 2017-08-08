using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Business.Importers
{
	public class MemoMapperBuilder
	{
		public MemoMapper Build(IEnumerable<MemoMapping> mappings)
		{
			MemoMapper mapper = new MemoMapper();
			foreach (var m in mappings)
				mapper.Add(m.IncomingMemo, new MemoMapperItem()
				{
					MappedAccount = m.MappedAccount,
					MappedCenter = m.MappedCenter,
					MappedMemo = m.MappedMemo
				});
			return mapper;
		}
	}
}

using System;
using System.Collections.Generic;

namespace FinancialControl.Business.Importers
{
	public class MemoMapper
	{
		private Dictionary<string, MemoMapperItem> map = new Dictionary<string, MemoMapperItem>();

		public void Add(string pattern, MemoMapperItem item)
		{
			this.map.Add(pattern, item);
		}

		public MemoMapperItem Map(string memo)
		{
			MemoMapperItem item = null;
			foreach (string regex in this.map.Keys)
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(memo, regex, 
					System.Text.RegularExpressions.RegexOptions.IgnoreCase))
				{
					item = this.map[regex];
					break;
				}
			}
			return item;
		}
	}
}

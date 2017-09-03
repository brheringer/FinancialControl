using System;
using FinancialControl.Model;

namespace FinancialControl.Business.Importers
{
	public class MemoMapperItem
	{
		public Account MappedAccount { get; set; }
		public ResultCenter MappedCenter { get; set; }
		public string MappedMemo { get; set; }
	}
}

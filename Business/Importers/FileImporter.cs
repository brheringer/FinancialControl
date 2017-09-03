using System;
using System.Collections.Generic;
using FinancialControl.Model;

namespace FinancialControl.Business.Importers
{
	public interface FileImporter
	{
		string FileContent { get; set; }
		Account TempAccount { get; set; }
		ResultCenter TempCenter { get; set; }
		string CurrentUser { get; set; }
		MemoMapper MemoMapper { get; set; }
		List<Entry> GeneratedEntries { get;  }

		void Import();

	}
}

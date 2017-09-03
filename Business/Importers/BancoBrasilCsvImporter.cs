using System;
using System.Collections.Generic;
using System.Text;
using FinancialControl.Model;

namespace FinancialControl.Business.Importers
{
	public class BancoBrasilCsvImporter
		: FileImporter
	{
		public string FileContent { get; set; }
		public Account TempAccount { get; set; }
		public ResultCenter TempCenter { get; set; }
		public string CurrentUser { get; set; }
		public MemoMapper MemoMapper { get; set; }
		public List<Entry> GeneratedEntries { get; private set; }

		public void Import()
		{
			this.GeneratedEntries = new List<Entry>();

			using (System.IO.StringReader sr = new System.IO.StringReader(this.FileContent))
			{
				sr.ReadLine(); //a primeira linha é o cabeçalho e não serve pra nada
				string line = sr.ReadLine();
				while (line != null)
				{
					string[] tokens = TokenizeLine(line);
					DateTime date = ReadDate(tokens);
					string memo = ReadMemo(tokens);
					decimal value = ReadValue(tokens);
					Entry entry = GenerateEntry(date, memo, value);
					this.GeneratedEntries.Add(entry);

					line = sr.ReadLine();
				}
			}
		}

		private Entry GenerateEntry(DateTime date, string memo, decimal value)
		{
			Account account = this.TempAccount;
			ResultCenter center = this.TempCenter;

			MemoMapperItem mappingItem = null;
			if (this.MemoMapper != null)
			{
				mappingItem = this.MemoMapper.Map(memo);
				if (mappingItem != null)
				{
					if (!string.IsNullOrEmpty(mappingItem.MappedMemo))
						memo = mappingItem.MappedMemo;
					if (mappingItem.MappedCenter != null)
						center = mappingItem.MappedCenter;
					if (mappingItem.MappedAccount != null)
						account = mappingItem.MappedAccount;
				}
			}

			Entry entry = new Entry();
			entry.Date = date;
			entry.Value = Math.Abs(value);
			entry.Memo = memo;
			entry.User = this.CurrentUser;
			entry.Account = account;
			entry.Center = center;
			return entry;
		}

		private string[] TokenizeLine(string line)
		{
			//cvs do BB: valores entre aspas, separados por vírgula
			//"Data","Dependencia Origem","Histórico","Data do Balancete","Número do documento","Valor"
			string[] tokens = line.Split(',');
			return tokens;
		}

		private DateTime ReadDate(string[] tokens)
		{
			//data no formato americano
			DateTime date;
			string sDate = tokens[0].Replace("\"", string.Empty);
			date = Convert.ToDateTime(sDate,
				System.Globalization.CultureInfo
				.GetCultureInfo("en-US").DateTimeFormat);
			return date;
		}

		private string ReadMemo(string[] tokens)
		{
			//histórico do banco.
			string memo;
			memo = tokens[2].Replace("\"", string.Empty);
			return memo;
		}

		private decimal ReadValue(string[] tokens)
		{
			//valor
			decimal value;
			string sValue = tokens[5].Replace("\"", string.Empty).Replace(".", ",");
			value = Convert.ToDecimal(sValue);
			return value;
		}

		private void FileSiege()
		{
			if (string.IsNullOrEmpty(this.FileContent))
				throw new InvalidOperationException("File importer needs the file path.");

			if (!System.IO.File.Exists(this.FileContent))
				throw new ArgumentException("The file path does not exist.");

			if (this.TempAccount == null || this.TempCenter == null || string.IsNullOrEmpty(this.CurrentUser))
				throw new InvalidOperationException("File importer needs account, center, user.");
		}
	}
}
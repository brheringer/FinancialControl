using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;
using FinancialControl.Business.Importers;

namespace FinancialControl.Business.Tests
{
	[TestClass]
	public class BancoBrasilCsvImporterTest
	{
		[TestMethod]
		public void TestImporterWithoutMapping()
		{
			BancoBrasilCsvImporter importador = new BancoBrasilCsvImporter();
			importador.FileContent = GetCSV();
			importador.TempCenter = new ResultCenter() { AutoId = 1, Description = "Centro Teste", Code = "X" };
			importador.TempAccount  = new Account() { AutoId = 2, Description = "Conta Teste", Structure = "1.9" };
			importador.CurrentUser = "tester";
			importador.MemoMapper = new MemoMapper();
			
			importador.Import();

			Assert.IsNotNull(importador.GeneratedEntries);
			Assert.AreEqual(9, importador.GeneratedEntries.Count);

			AssertEntry(importador.GeneratedEntries[0], "1.9", "X", new DateTime(2017, 1, 31), 5574.88M, "Saldo Anterior");
			AssertEntry(importador.GeneratedEntries[1], "1.9", "X", new DateTime(2017, 2, 1), 10.00M, "Compra com Cartão - 01/02 17:00 RESTAURANTE E LANCHO");
			AssertEntry(importador.GeneratedEntries[2], "1.9", "X", new DateTime(2017, 2, 1), 149.85M, "Compra com Cartão - 01/02 17:28 POSTO CIDADE NOVA");
			AssertEntry(importador.GeneratedEntries[3], "1.9", "X", new DateTime(2017, 2, 1), 200.00M, "Transferência on line - 01/02 2241      34336-6 FLAVIO DE ANDR");
			AssertEntry(importador.GeneratedEntries[4], "1.9", "X", new DateTime(2017, 2, 1), 185.12M, "Pagto Energia Elétrica");
			AssertEntry(importador.GeneratedEntries[5], "1.9", "X", new DateTime(2017, 2, 2), 4.00M, "Compra com Cartão - 02/02 18:10 RESTAURANTE E LANCHO");
			AssertEntry(importador.GeneratedEntries[6], "1.9", "X", new DateTime(2017, 2, 8), 441.44M, "CASSI");
			AssertEntry(importador.GeneratedEntries[7], "1.9", "X", new DateTime(2017, 2, 9), 137.01M, "Compra com Cartão - 09/02 19:06 POSTO SAGRADA FAM");
			AssertEntry(importador.GeneratedEntries[8], "1.9", "X", new DateTime(2017, 3, 1), 5836.11M, "S A L D O");
		}

		[TestMethod]
		public void TestImporterWithMapping()
		{
			BancoBrasilCsvImporter importer = new BancoBrasilCsvImporter();
			importer.FileContent = GetCSV();
			importer.TempCenter = new ResultCenter() { AutoId = 1, Description = "Centro Teste", Code = "X" };
			importer.TempAccount = new Account() { AutoId = 2, Description = "Conta Teste", Structure = "1.9" };
			importer.CurrentUser = "tester";

			MemoMapperItem itemGas = new MemoMapperItem();
			itemGas.MappedCenter = new ResultCenter() { AutoId = 2, Description = "Carro", Code = "C" }; ;
			itemGas.MappedAccount = new Account() { AutoId = 3, Description = "Combustível", Structure = "1.1" }; ;
			itemGas.MappedMemo = "Gas";

			MemoMapperItem itemFlavio = new MemoMapperItem();
			itemFlavio.MappedCenter = new ResultCenter() { AutoId = 3, Description = "Reino", Code = "R" }; ;
			itemFlavio.MappedAccount = new Account() { AutoId = 4, Description = "Flavio", Structure = "1.4" }; ;
			itemFlavio.MappedMemo = "Flavio Stevens";
			
			importer.MemoMapper = new MemoMapper();
			importer.MemoMapper.Add(".*posto.*", itemGas);
			importer.MemoMapper.Add(".*34336-6 FLAVIO.*", itemFlavio);

			importer.Import();

			Assert.IsNotNull(importer.GeneratedEntries);
			Assert.AreEqual(9, importer.GeneratedEntries.Count);

			AssertEntry(importer.GeneratedEntries[0], "1.9", "X", new DateTime(2017, 1, 31), 5574.88M, "Saldo Anterior");
			AssertEntry(importer.GeneratedEntries[1], "1.9", "X", new DateTime(2017, 2, 1), 10.00M, "Compra com Cartão - 01/02 17:00 RESTAURANTE E LANCHO");
			AssertEntry(importer.GeneratedEntries[2], "1.1", "C", new DateTime(2017, 2, 1), 149.85M, "Gas");
			AssertEntry(importer.GeneratedEntries[3], "1.4", "R", new DateTime(2017, 2, 1), 200.00M, "Flavio Stevens");
			AssertEntry(importer.GeneratedEntries[4], "1.9", "X", new DateTime(2017, 2, 1), 185.12M, "Pagto Energia Elétrica");
			AssertEntry(importer.GeneratedEntries[5], "1.9", "X", new DateTime(2017, 2, 2), 4.00M, "Compra com Cartão - 02/02 18:10 RESTAURANTE E LANCHO");
			AssertEntry(importer.GeneratedEntries[6], "1.9", "X", new DateTime(2017, 2, 8), 441.44M, "CASSI");
			AssertEntry(importer.GeneratedEntries[7], "1.1", "C", new DateTime(2017, 2, 9), 137.01M, "Gas");
			AssertEntry(importer.GeneratedEntries[8], "1.9", "X", new DateTime(2017, 3, 1), 5836.11M, "S A L D O");
		}

		private void AssertEntry(Entry entry, string accountStructure, string centerCode, DateTime date, decimal value, string memo)
		{
			Assert.IsNotNull(entry);
			Assert.IsNotNull(entry.Account);
			Assert.IsNotNull(entry.Center);
			Assert.AreEqual(accountStructure, entry.Account.Structure);
			Assert.AreEqual(centerCode, entry.Center.Code);
			Assert.AreEqual(date, entry.Date);
			Assert.AreEqual(value, entry.Value);
			Assert.AreEqual(memo, entry.Memo);
		}

		private string GetCSV()
		{
			return @"""Data"",""Dependencia Origem"",""Histórico"",""Data do Balancete"",""Número do documento"",""Valor"",
""31/01/2017"","""",""Saldo Anterior"","""",""0"",""5574.88"",
""01/02/2017"",""316-6"",""Compra com Cartão - 01/02 17:00 RESTAURANTE E LANCHO"","""",""361216"",""-10.00"",
""01/02/2017"",""316-6"",""Compra com Cartão - 01/02 17:28 POSTO CIDADE NOVA"","""",""662916"",""-149.85"",
""01/02/2017"",""316-6"",""Transferência on line - 01/02 2241      34336-6 FLAVIO DE ANDR"","""",""522241000034336"",""-200.00"",
""01/02/2017"","""",""Pagto Energia Elétrica"","""",""2534"",""-185.12"",
""02/02/2017"",""316-6"",""Compra com Cartão - 02/02 18:10 RESTAURANTE E LANCHO"","""",""165449"",""-4.00"",
""08/02/2017"","""",""CASSI"","""",""18524"",""-441.44"",
""09/02/2017"",""316-6"",""Compra com Cartão - 09/02 19:06 POSTO SAGRADA FAM"","""",""168761"",""-137.01"",
""01/03/2017"","""",""S A L D O"","""",""0"",""5836.11"",
";
		}
	}
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;
using FinancialControl.Business.Importers;

namespace FinancialControl.Business.Tests
{
	[TestClass]
	public class MemoMapperTest
	{
		[TestMethod]
		public void MappingTest()
		{
			MappingTest("asdf brasilprev asdf", ".*Brasilprev.*", "Brasilprev", "Brasilprev", "Monick");
			MappingTest("29374 34336-6 flavio asdf", ".*34336-6 FLAVIO.", "Suporte Flávio", "Flávio Vital", "Reino");
			MappingTest("9342restaurante wall street23145", ".*STREET.*", "Restaurante Wall Street", "Almoço fora", "Emerson");
		}

		private void MappingTest(string input, string padrao, string historico, string conta, string centro)
		{
			MemoMapper m = new MemoMapper();
			m.Add(padrao, CreateItem(historico, conta, centro));
			var item = m.Map(input);
			Assert.IsNotNull(item);
			Assert.IsNotNull(item.MappedAccount);
			Assert.IsNotNull(item.MappedCenter);
			Assert.AreEqual(item.MappedAccount.Description, conta);
			Assert.AreEqual(item.MappedCenter.Description, centro);
			Assert.AreEqual(item.MappedMemo, historico);
		}

		private MemoMapperItem CreateItem(string historico, string conta, string centro)
		{
			MemoMapperItem item = new MemoMapperItem();
			item.MappedAccount = new Account() { Description = conta };
			item.MappedCenter = new ResultCenter() { Description = centro };
			item.MappedMemo = historico;
			return item;
		}
	}
}

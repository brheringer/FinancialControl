using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialControl.Model;

namespace FinancialControl.Persistence.NHPersistence.Tests
{
	[TestClass]
	public class ResultCenterDAOTest
		: BaseTest
	{
		private const string USER = "tester";

		[TestMethod]
		public void TestNewUpdate()
		{
			CreateCenter(1);

			var centers = LoadAll();
			Assert.IsNotNull(centers);
			Assert.AreEqual(1, centers.Count);
		}

		[TestMethod]
		public void TestDelete()
		{
			TransactionMngrInstance.BeginTransaction();
			CreateCenter(1);
			TransactionMngrInstance.Commit();
			
			TransactionMngrInstance.BeginTransaction();
			var center = TransactionMngrInstance.Factory.ResultCenterDAO.Load(1);
			TransactionMngrInstance.Factory.ResultCenterDAO.Delete(center);
			TransactionMngrInstance.Commit();

			var centers = LoadAll();
			Assert.IsNotNull(centers);
			Assert.AreEqual(0, centers.Count);
		}

		[TestMethod]
		public void TestLoadAll()
		{
			CreateCenter(1);
			CreateCenter(2);
			CreateCenter(3);

			var centers = LoadAll();

			Assert.IsNotNull(centers);
			Assert.AreEqual(3, centers.Count);
		}

		private IList<ResultCenter> LoadAll()
		{
			var centers = TransactionMngrInstance.Factory.ResultCenterDAO.LoadAll(USER);
			return centers;
		}

		[TestMethod]
		public void TestLoadFirstMatch()
		{
			//build
			CreateCenter(1);
			CreateCenter(2);
			CreateCenter(3);

			//operate
			var center = TransactionMngrInstance.Factory.ResultCenterDAO.LoadFirstMatch("ent", USER);

			//check
			Assert.IsNotNull(center);
			Assert.AreEqual(1, center.AutoId);
		}

		[TestMethod]
		public void TestLoadFirstMatchWithNoMatch()
		{
			CreateCenter(1);
			CreateCenter(2);
			CreateCenter(3);

			var center = TransactionMngrInstance.Factory.ResultCenterDAO.LoadFirstMatch("asdf", USER);

			Assert.IsNull(center);
		}

		[TestMethod]
		public void TestSearchByCode()
		{
			CreateCenter(1);
			CreateCenter(2);
			CreateCenter(3);

			var centers = TransactionMngrInstance.Factory.ResultCenterDAO.Search("2", null, USER);

			Assert.AreEqual(1, centers.Count);
			Assert.AreEqual("2", centers[0].Code);
		}

		[TestMethod]
		public void TestSearchByDescription()
		{
			CreateCenter(1);
			CreateCenter(2);
			CreateCenter(3);

			var centers = TransactionMngrInstance.Factory.ResultCenterDAO.Search(null, "enter 2", USER);

			Assert.AreEqual(1, centers.Count);
			Assert.AreEqual("2", centers[0].Code);
		}

		[TestMethod]
		public void TestSmartSearch()
		{
			CreateCenter(1, "House");
			CreateCenter(10, "Car");
			CreateCenter(11, "Something");
			CreateCenter(41, "Family");
			CreateCenter(52, "Others");
			CreateCenter(63, "Whatever with s 1");

			var centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch("1", USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(1, centers.Count, "Result center SmartSearch(1) should find only 1, since it considers only the description.");

			centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch("10", USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(0, centers.Count, "Result center SmartSearch(10) should find nothing, since it considers only the description.");

			centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch("s", USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(4, centers.Count);

			centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch("%", USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(6, centers.Count, "It should find all the accounts when it uses wildcard char.");

			centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch("", USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(6, centers.Count, "It should find all the accounts when it uses empty.");

			centers = TransactionMngrInstance.Factory.ResultCenterDAO.SmartSearch(null, USER);
			Assert.IsNotNull(centers);
			Assert.AreEqual(6, centers.Count, "It should find 0 account when it uses null.");

		}

		private void CreateCenter(long id)
		{
			ResultCenter center = new ResultCenter();
			center.Code = id.ToString();
			center.Description = "Center " + id;
			center.User = USER;

			TransactionMngrInstance.Factory.ResultCenterDAO.Update(center);
		}

		private void CreateCenter(int id, string description)
		{
			ResultCenter center = new ResultCenter();
			center.Code = id.ToString();
			center.Description = description;
			center.User = USER;

			TransactionMngrInstance.Factory.ResultCenterDAO.Update(center);
		}
	}
}

using System;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class BuyTest
	{
		[TestMethod]
		public void TransactionTest1()
		{
			// player a sell A1 to system.
			Player a = new("A", 1000);
			Player b = new("B", 2000);
			Player c = new("C", 3000);

			a.AddLandContract(new(1000, "A1"));
			var landContract = a.SellLandContract("A1");
			if (!landContract.HasOutcry())
			{
				landContract.Sell();
			}
			Assert.AreEqual(a.FindLandContract("A1"), false);
			Assert.AreEqual(a.Money, 1700);
		}

		[TestMethod]
		public void TransactionTest2()
		{
			// player a sell A1 to b with $600.
			Player a = new("A", 1000);
			Player b = new("B", 2000);
			Player c = new("C", 3000);

			a.AddLandContract(new(1000, "A1"));
			var landContract = a.SellLandContract("A1");

			landContract.SetOutcry(b, 800);
			landContract.Sell();

			Assert.AreEqual(a.FindLandContract("A1"), false);
			Assert.AreEqual(b.FindLandContract("A1"), true);
			Assert.AreEqual(a.Money, 1800);
			Assert.AreEqual(b.Money, 1200);
		}

		[TestMethod]
		public void TransactionTest3()
		{
			// player a sell A1, b has outcry with $3000, but b has not enough money so that fail to outcry
			Player a = new("A", 1000);
			Player b = new("B", 2000);
			Player c = new("C", 3000);

			a.AddLandContract(new(1000, "A1"));
			var landContract = a.SellLandContract("A1");

			landContract.SetOutcry(b, 3000);
			landContract.Sell();

			Assert.AreEqual(a.FindLandContract("A1"), false);
			Assert.AreEqual(a.Money, 1700);
			Assert.AreEqual(b.Money, 2000);
			Assert.AreEqual(b.FindLandContract("A1"), false);
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class SettlementTest
	{
		[TestMethod]
		public void BankruptTest()
		{
			/** 
			 *  B is bankrupt and then C is bankrupt,
			 *  The settlement is ranked as ACB
			 */
			Game game = new("Test");
			Player a = new("A");
            Player b = new("B", 0);
            Player c = new("C", 0);

			game.AddPlayers(a, b, c);

			game.UpdatePlayerState(b);
			game.UpdatePlayerState(c);
            
			game.Settlement();

            Assert.AreEqual(1, game.RankMap[a]);
            Assert.AreEqual(3, game.RankMap[b]);
            Assert.AreEqual(2, game.RankMap[c]);

        }

		[TestMethod]
		public void RankTest()
		{
			/**
			 * Players ABCD, game end with 
			 *  A with money = 5000
			 *  B with money = 4000
			 *  C with money = 3000
			 *  D with money = 2000
			 *  Rank is A,B,C,D
			 */
			Game game = new("Test");
			Player a = new("A");
            Player b = new("B");
            Player c = new("C");
            Player d = new("D");

			game.AddPlayers(a, b, c, d);

			var landContractA1 = new LandContract(2000, "A1");
			landContractA1.Upgrade();
			a.AddLandContract(landContractA1);
			a.AddMoney(1000);

			var landContractB1 = new LandContract(2000, "B1");
			landContractB1.Upgrade();
			b.AddLandContract(landContractB1);

			var landContractC1 = new LandContract(2000, "C1");
			c.AddLandContract(landContractC1);
			c.AddMoney(1000);

			var landContractD1 = new LandContract(2000, "D1");
			d.AddLandContract(landContractD1);

			game.Settlement();

			var ranking = game.RankMap;

			Assert.AreEqual(1, ranking[a]);
            Assert.AreEqual(2, ranking[b]);
            Assert.AreEqual(3, ranking[c]);
            Assert.AreEqual(4, ranking[d]);
        }
	}
}


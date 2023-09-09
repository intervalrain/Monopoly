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
			Game game = new();
			Player? playerA = new("A");
            Player? playerB = new("B");
            Player? playerC = new("C");
            
			game.AddPlayer(playerA);
			game.AddPlayer(playerB);
			game.AddPlayer(playerC);

			game.SetState(playerB, PlayerState.Bankrupt);
            game.SetState(playerC, PlayerState.Bankrupt);
            
			game.Settlement();

            Assert.AreEqual(1, game.RankList[playerA]);
            Assert.AreEqual(3, game.RankList[playerB]);
            Assert.AreEqual(2, game.RankList[playerC]);

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
			Game game = new();
			Player? playerA = new("A");
            Player? playerB = new("B");
            Player? playerC = new("C");
            Player? playerD = new("D");
           
			game.AddPlayer(playerA);
			game.AddPlayer(playerB);
			game.AddPlayer(playerC);
			game.AddPlayer(playerD);

			var landContractA1 = new LandContract(2000, playerA);
			landContractA1.Upgrade();
			playerA?.AddLandContract(landContractA1);
			playerA?.AddMoney(1000);

			var landContractB1 = new LandContract(2000, playerB);
			landContractB1.Upgrade();
			playerB?.AddLandContract(landContractB1);

			var landContractC1 = new LandContract(2000, playerC);
			playerC?.AddLandContract(landContractC1);
			playerC?.AddMoney(1000);

			var landContractD1 = new LandContract(2000, playerD);
			playerD?.AddLandContract(landContractD1);

			game.Settlement();

			var ranking = game.RankList;

			Assert.AreEqual(1, ranking[playerA]);
            Assert.AreEqual(2, ranking[playerB]);
            Assert.AreEqual(3, ranking[playerC]);
            Assert.AreEqual(4, ranking[playerD]);
        }
	}
}


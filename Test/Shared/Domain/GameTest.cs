using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class GameTest
	{
		[TestMethod]
		public void SettlementTest()
		{
			/** 
			 *  B is bankrupt and then C is bankrupt,
			 *  The settlement is ranked as ACB
			 */
			Game game = new();
			string id_a = "A";
			string id_b = "B";
			string id_c = "C";

			game.AddPlayer(id_a);
			game.AddPlayer(id_b);
			game.AddPlayer(id_c);

			game.SetState(id_b, PlayerState.Bankrupt);
            game.SetState(id_c, PlayerState.Bankrupt);

			game.Settlement();

			Player? playerA = game.FindPlayerById(id_a);
            Player? playerB = game.FindPlayerById(id_b);
            Player? playerC = game.FindPlayerById(id_c);
            Assert.AreEqual(1, game.RankList[playerA]);
            Assert.AreEqual(3, game.RankList[playerB]);
            Assert.AreEqual(2, game.RankList[playerC]);

        }
		[TestMethod]
		public void SettlementTest2()
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
			string id_a = "A";
			string id_b = "B";
			string id_c = "C";
			string id_d = "D";

			game.AddPlayer(id_a);
			game.AddPlayer(id_b);
			game.AddPlayer(id_c);
			game.AddPlayer(id_d);

			var playerA = game.FindPlayerById(id_a);
			var landContractA1 = new LandContract(2000, playerA);
			landContractA1.Upgrade();
			playerA?.AddLandContract(landContractA1);
			playerA?.AddMoney(1000);

			var playerB = game.FindPlayerById(id_b);
			var landContractB1 = new LandContract(2000, playerB);
			landContractB1.Upgrade();
			playerB?.AddLandContract(landContractB1);

			var playerC = game.FindPlayerById(id_c);
			var landContractC1 = new LandContract(2000, playerC);
			playerC?.AddLandContract(landContractC1);
			playerC?.AddMoney(1000);

			var playerD = game.FindPlayerById(id_d);
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


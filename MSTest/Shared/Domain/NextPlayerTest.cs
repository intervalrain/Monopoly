//using System;
//using Shared.Domain;

//namespace Test.Shared.Domain
//{
//	[TestClass]
//	public class NextPlayerTest
//	{
//		[TestMethod]
//		[Description(
//			"""
//			Given:  玩家A,B,C
//			        目前輪到A
//			When:   玩家A結束回合
//			Then:   輪到玩家B的回合
//			""")]
//		public void 結束回合之後輪到下一玩家的回合()
//		{
//			Map map = new Map(Utils.SevenXSevenMap);
//			Game game = new Game("test", map);
//			Player a = new("A"),
//				   b = new("B"),
//				   c = new("C");
//			game.AddPlayers(a, b, c);
			
//		}

//        [TestMethod]
//        [Description(
//			"""
//			Given:  玩家A,B,C
//			        目前輪到A
//					玩家B已破產
//			When:   玩家A結束回合
//			Then:   輪到玩家C的回合
//			""")]
//        public void 結束回合之後輪到下一玩家的回合_B破產()
//        {

//        }

//        [TestMethod]
//        [Description(
//			"""
//			Given:  玩家A,B,C
//			        目前輪到A
//					玩家B在監獄
//			When:   玩家A結束回合
//			Then:   輪到玩家C的回合
//			""")]
//        public void 結束回合之後輪到下一玩家的回合_B在監獄()
//        {

//        }
//    }
//}


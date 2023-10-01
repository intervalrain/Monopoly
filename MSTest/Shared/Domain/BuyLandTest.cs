using System;
using Shared.Domain;
namespace Test.Shared.Domain
{
	[TestClass]
	public class BuyLandTest
	{
		[TestMethod]
		[Description(
			"""
			Given:  玩家A在空地F4上
			        玩家A有5000元
					F4的價值為1000元
			When:   玩家A購買土地
			Then:   玩家A擁有土地
			""")]
		public void BuyLand()
		{
			// Arrange
			Map map = new Map(Utils.SevenXSevenMap);
			Game game = new Game("Test", map);
			Player a = new("A", 5000);
			game.AddPlayer(a);
			game.Initial();
			game.SetPlayerToBlock(a, "F4", Direction.Up);

			// Act
			game.BuyLand(a, "F4");

			// Assert
			Assert.AreEqual(4000, a.Money);
			Assert.AreEqual(1, a.LandContracts.Count());
			Assert.IsNotNull(a.FindLandContract("F4"));
		}
	}
}


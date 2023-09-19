using System;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class BankruptTest
	{
		[TestMethod]
		public void LackOfHouseAndMoney()
		{
			// if a player lack of both money and house, set the player's state bankrupt
			Player playerA = new("A", 0);
			var game = new Game("Test");

			game.UpdatePlayerState(playerA);

			Assert.AreEqual(playerA.State, PlayerState.Bankrupt);
		}
	}
}


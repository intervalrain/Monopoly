using System;
using Shared.Domain;
using Test.Shared.Utils;

namespace Test.Shared.Domain
{
	[TestClass]
	public class MoveChessTest
	{
		[TestMethod]
		public void MoveTest()
		{
            // A dice can only roll 6.
            Game game = new Game("Test", new Map(Utils.Utils._7x7Map), new DiceSetting(1, 6, 6));
            Player a = new("A");
            game.AddPlayer(a);
            game.Initial();
            game.SetPlayerToBlock(a, "F4", Direction.Up);

            // Act
            game.PlayerRollDice(a.Id);
            game.PlayerMoveChess(a.Id);

            // Assert
            Assert.AreEqual("A4", game.GetPlayerPosition("A"));
		}
    }
}


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
            Game game = new Game("Test", new Map(MapLibrary._7x7Map));
            Player a = new("A");
            game.AddPlayer(a);

            game.SetPlayerToBlock(a, "F4", Direction.Enumerates.Up);
            
            int point = 6;
            game.MovePlayer(a, point);

            Assert.AreEqual("A4", game.GetPlayerPosition("A"));
		}
    }
}


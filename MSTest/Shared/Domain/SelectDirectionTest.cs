using System;
using Shared.Domain;
using Test.Shared.Utils;

namespace Test.Shared.Domain
{
    [TestClass]
	public class SelectDirectionTest
	{
        [TestMethod]
		public void DirectionTest()
		{
            // player a at parking lot, facing down, choosing left as new direction
            Game game = new Game("Test", new Map(MapLibrary._7x7Map));
            Player a = new("A");
            game.AddPlayer(a);

            game.SetPlayerToBlock(a, "ParkingLot", Direction.Enumerates.Down);

            game.SelectionDirection(a, Direction.Enumerates.Left);
            Assert.AreEqual("ParkingLot", game.GetPlayerPosition("A"));

            Direction.Enumerates direction = game.GetPlayerDirection("A");
            Assert.AreEqual(Direction.Enumerates.Left, direction);
		}
    }
}


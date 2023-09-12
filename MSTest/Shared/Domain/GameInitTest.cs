using System;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class GameInitTest
	{
		[TestMethod]
		public void InitTest()
		{
            // player a,b,c,d have 15000 money at start when the game starts
            Game game = new Game(new Map(_7x7Map));
            Player a = new("A");
            Player b = new("B");
            Player c = new("C");
            Player d = new("D");

            game.AddPlayers(a, b, c, d);
            game.Initial();

            Assert.AreEqual(15000, a.Money);
            Assert.AreEqual(15000, b.Money);
            Assert.AreEqual(15000, c.Money);
            Assert.AreEqual(15000, d.Money);

            Assert.AreEqual("Start", game.GetPlayerPosition(a));
            Assert.AreEqual("Start", game.GetPlayerPosition(b));
            Assert.AreEqual("Start", game.GetPlayerPosition(c));
            Assert.AreEqual("Start", game.GetPlayerPosition(d));
        }


        IBlock?[][] _7x7Map => new IBlock?[][]
        {
            new IBlock?[] { new Block("Start"),    new Block("A1"), new Block("Station1"), new Block("A2"), new Block("A3"),         null,            null },
            new IBlock?[] { new Block("F4"),       null,            null,                  null,            new Block("A4"),         null,            null },
            new IBlock?[] { new Block("Station4"), null,            new Block("B5"),       new Block("B6"), new Block("ParkingLot"), new Block("C1"), new Block("C2") },
            new IBlock?[] { new Block("F3"),       null,            new Block("B4"),       null,            new Block("B1"),         null,            new Block("C3") },
            new IBlock?[] { new Block("F2"),       new Block("F1"), new Block("Prison"),   new Block("B3"), new Block("B2"),         null,            new Block("Station2") },
            new IBlock?[] { null,                  null,            new Block("E3"),       null,            null,                    null,            new Block("D1") },
            new IBlock?[] { null,                  null,            new Block("E2"),       new Block("E1"), new Block("Station3"),   new Block("D3"), new Block("D2") }
        };
    }
}


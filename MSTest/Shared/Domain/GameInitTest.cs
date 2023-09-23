using System;
using Shared.Domain;
using Test.Shared.Utils;

namespace Test.Shared.Domain
{
	[TestClass]
	public class GameInitTest
	{
		[TestMethod]
		public void InitTest()
		{
            // player a,b,c,d have 15000 money at start when the game starts
            Game game = new Game("Test", new Map(Utils.Utils._7x7Map));
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

            Assert.AreEqual("Start", game.GetPlayerPosition("A"));
            Assert.AreEqual("Start", game.GetPlayerPosition("B"));
            Assert.AreEqual("Start", game.GetPlayerPosition("C"));
            Assert.AreEqual("Start", game.GetPlayerPosition("D"));
        }
    }
}


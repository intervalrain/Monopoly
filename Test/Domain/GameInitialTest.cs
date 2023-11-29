using System;
using Shared.Domain;
using Shared.Domain.Maps;

namespace Test.Domain;

[TestClass]
public class GameInitialTest
{
	[TestMethod]
	[Description(
		"""
		Given: 玩家ABCD，遊戲初始時，各有15000元，且位置在起點
		 When:
		 Then:
		""")]
	public void 玩家ABCD_遊戲初始設定_每位玩家有15000_位置在起點()
	{
		Map map = new Map(_7x7Map.Standard7x7);
		Game game = new Game(map);
		Player a = new("A", 15000),
			   b = new("B", 15000),
			   c = new("C", 15000),
			   d = new("D", 15000);
		game.AddPlayers(a, b, c, d);

		Assert.AreEqual(15000, a.Money);
        Assert.AreEqual(15000, b.Money);
        Assert.AreEqual(15000, c.Money);
        Assert.AreEqual(15000, d.Money);
		Assert.AreEqual("Start", game.GetPlayerPosition(a).Id);
        Assert.AreEqual("Start", game.GetPlayerPosition(b).Id);
        Assert.AreEqual("Start", game.GetPlayerPosition(c).Id);
        Assert.AreEqual("Start", game.GetPlayerPosition(d).Id);
    }
}

using Domain;
using Domain.Maps;
using Domain.Enums;

namespace Test.Domain;

[TestClass]
public class GameInitialTest
{
	[TestMethod]
	[Description(
		"""
		Given: 玩家 ABCD，遊戲初始時，各有15000元，且位置在起點
		 When:
		 Then:
		""")]
	public void 玩家ABCD_遊戲初始設定_每位玩家有15000_位置在起點()
	{
		Map map = new Map(_7x7Map.Standard7x7);
		Game game = new Game("007", map);
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

	[TestMethod]
	[Description(
		"""
		Given: 玩家 ABCD，遊戲初始時，各有5000, 6000, 7000, 8000元，
		       各自位在 A1, B1, C1, D1，
			   面朝上、下、左、右
		 When:
		 Then:
		""")]
	public void 設置玩家ABCD到不同位置_面向不同方向()
	{
		Map map = new Map(_7x7Map.Standard7x7); 
		Game game = new Game("015", map);
		Player a = new Player("A")
					   .Has(5000)
					   .At(map.FindBlockById("A1"))
					   .Face(Direction.Up);
		Player b = new Player("B")
					   .Has(6000)
					   .At(map.FindBlockById("B1"))
					   .Face(Direction.Down);
		Player c = new Player("C")
					   .Has(7000)
					   .At(map.FindBlockById("C1"))
					   .Face(Direction.Left);
		Player d = new Player("D")
					   .Has(8000)
					   .At(map.FindBlockById("D1"))
					   .Face(Direction.Right);
		game.AddPlayers(a, b, c, d);

		Assert.AreEqual(5000, a.Money);
        Assert.AreEqual(6000, b.Money);
        Assert.AreEqual(7000, c.Money);
        Assert.AreEqual(8000, d.Money);
		Assert.AreEqual("A1", a.Position.Id);
        Assert.AreEqual("B1", b.Position.Id);
        Assert.AreEqual("C1", c.Position.Id);
        Assert.AreEqual("D1", d.Position.Id);
		Assert.AreEqual(Direction.Up, a.Direction);
        Assert.AreEqual(Direction.Down, b.Direction);
        Assert.AreEqual(Direction.Left, c.Direction);
        Assert.AreEqual(Direction.Right, d.Direction);
    }
}

using Shared.Domain;
using Shared.Domain.Maps;

namespace Test.Domain;

[TestClass]
public class BuyRealEstateTest
{
    [TestMethod]
    [Description(
    """
		Given: 玩家 A 有 5000 元，玩家 B 有 5000 元與空地 A1
		 When: A 購買空地 A1
		 Then: 空地仍屬於 B，A 擁有 5000 元，B 擁有 5000 元
		""")]
    public void 玩家A購買已被佔有的地()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(map);
        Player a = new Player("A", 5000);
        Player b = new Player("B", 5000);
        game.AddPlayers(a, b);
		var A1 = map.FindBlockById("A1");
		A1.Contract.SetOwner(b);

		game.PlayerBuyLand(a, A1);
		Assert.AreEqual(b.Id, A1.Contract.Owner!.Id);
        Assert.AreEqual(5000, a.Money);
        Assert.AreEqual(5000, b.Money);
    }

    [TestMethod]
    [Description(
    """
		Given: 玩家 A 有 5000 元
		 When: A 購買空地 A1
		 Then: 空地屬於 A，A 剩餘 4000 元
		""")]
    public void 玩家A購買空地()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(map);
        Player a = new Player("A", 5000);
        
        game.AddPlayer(a);
        var A1 = map.FindBlockById("A1");

        game.PlayerBuyLand(a, A1);
        Assert.AreEqual(a.Id, A1.Contract.Owner!.Id);
        Assert.AreEqual(4000, a.Money);
    }

    [TestMethod]
    [Description(
    """
		Given: 玩家 A 有 800 元
		 When: A 購買空地 A1
		 Then: 空地仍屬無主地，A 剩餘 800 元
		""")]
    public void 玩家A財產不足無法買地()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(map);
        Player a = new Player("A", 800);

        game.AddPlayer(a);
        var A1 = map.FindBlockById("A1");

        game.PlayerBuyLand(a, A1);
        Assert.AreEqual(null, A1.Contract.Owner);
        Assert.AreEqual(800, a.Money);
    }

    [TestMethod]
    [Description(
    """
		Given: 玩家 A 有 800 元
		 When: A 販賣空地 A1
		 Then: 空地屬無主地，A 剩餘 1800 元
		""")]
    public void 玩家A販賣空地()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(map);
        Player a = new Player("A", 800);

        game.AddPlayer(a);
        var A1 = map.FindBlockById("A1");
        A1.Contract.SetOwner(a);

        game.PlayerSellLand(a, A1);
        Assert.AreEqual(null, A1.Contract.Owner);
        Assert.AreEqual(1800, a.Money);
    }

    [TestMethod]
    [Description(
"""
		Given: 玩家 A 有 800 元
		 When: A 販賣有房地 A1 (level = 1)
		 Then: 空地屬無主地，A 剩餘 1800 元
		""")]
    public void 玩家A販賣房屋與地()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(map);
        Player a = new Player("A", 800);

        game.AddPlayer(a);
        var A1 = map.FindBlockById("A1");
        A1.Contract.SetOwner(a);
        A1.Contract.Upgrade();

        game.PlayerSellLand(a, A1);
        Assert.AreEqual(null, A1.Contract.Owner);
        Assert.AreEqual(2800, a.Money);
    }
}

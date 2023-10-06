using Application.Domain;

namespace Test.Application.Domain;

[TestClass]
public class BuyLandTest
{
	[TestMethod]
    [Description(
    """
    Given:  玩家A資產5000元，沒有房地產
            目前輪到A，A在F4上
            A1是空地，購買價1000元
    When:   玩家A購買土地
    Then:   玩家A持有金額為4000
            玩家A持有房地產數量為1
            玩家A持有房地產為F4
    """)]
    public void 玩家在空地上可以購買土地()
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

    [TestMethod]
    [Description(
    """
    Given:  玩家A資產500元,沒有房地產
            目前輪到A,A在F4上
            A1是空地,購買價1000元
    When:   玩家A購買土地
    Then:   顯示錯誤訊息"金額不足"
            玩家A持有金額為500
            玩家A持有房地產數量為0
    """)]
    public void 金錢不夠無法購買土地()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map);
        Player a = new("A", 500);
        game.AddPlayer(a);
        game.Initial();
        game.SetPlayerToBlock(a, "F4", Direction.Up);

        // Act
        Assert.ThrowsException<Exception>(() => game.BuyLand(a, "F4"), "金額不足");

        // Assert
        Assert.AreEqual(a.Money, 500);
        Assert.AreEqual(a.LandContracts.Count, 0);
    }


    [TestMethod]
    [Description(
    """
    Given:  玩家A資產5000元，沒有房地產
            玩家B資產5000元，擁有房地產A4
            目前輪到A，A在F4上
    When:   玩家A購買土地F4
    Then:   顯示錯誤訊息"非空地"
            玩家A持有金額為5000，持有房地產數量為0
            玩家B持有金額為5000，持有房地產數量為1，持有F4
    """)]
    public void 無法購買非空地土地()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map);
        Player a = new("A", 5000),
                b = new("B", 5000);
        game.AddPlayers(a, b);
        game.Initial();
        game.SetPlayerToBlock(a, "F4", Direction.Up);
        Land F4 = (Land)map.FindBlockById("F4");
        b.AddLandContracts(F4);

        // Act
        Assert.ThrowsException<Exception>(() => game.BuyLand(a, "F4"), "非空地");

        // Assert
        Assert.AreEqual(5000, a.Money);
        Assert.AreEqual(0, a.LandContracts.Count);
        Assert.AreEqual(5000, b.Money);
        Assert.AreEqual(1, b.LandContracts.Count);
        Assert.IsTrue(b.LandContracts.Any(l => l.Land.Id == "F4"));
    }

    [TestMethod]
    [Description(
    """
    Given:  玩家A資產5000元，沒房地產
            目前輪到A，A在F2上
    When:   玩家A購買土地F4
    Then:   顯示錯誤訊息"必須在購買的土地上才可以購買"
            玩家A持有金額5000，持有房地產數量為0
    """)]
    public void 無法購買非腳下的土地()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map);
        Player a = new("A", 5000);
        game.AddPlayer(a);
        game.Initial();
        game.SetPlayerToBlock(a, "F2", Direction.Up);

        // Act
        Assert.ThrowsException<Exception>(() => game.BuyLand(a, "F4"), "必須在購買的土地上才可以購買");

        // Assert
        Assert.AreEqual(5000, a.Money);
        Assert.AreEqual(0, a.LandContracts.Count);
    }
}

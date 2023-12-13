using Domain;
using Domain.Maps;
using Domain.Enums;
using Test.Common;


namespace Test.Domain;

[TestClass]
public class PayTollTest
{
    [TestMethod]
    [Description(
        """
        Given: 玩家A, B
               玩家A持有的金額 1000, 房地產 A4, 地價 1000, 沒有房子
               玩家B持有的金額 1000
               B的回合移動到A4
         When: B扣除過路費1000 * 5% = 50給A
         Then: 玩家A持有金額為1000+50 = 1050
               玩家A持有金額為1000-50 = 950
        """)]
    public void 玩家付過路費_無房_無同地段()
    {
        // Arrange
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game("T01", map);

        Player a = new Player("A", 1000);
        Player b = new Player("B", 1000);

        game.AddPlayers(a, b);
        game.CurrentPlayer = a;
        var A4 = map.FindBlockById("A4");
        game.SetPlayerToBlock(a, "A4", Direction.Down);
        A4.Contract.SetOwner(a);

        game.CurrentPlayer = b;
        game.SetPlayerToBlock(b, "A2", Direction.Right);
        var dices = Utils.MockDice(2);
        game.SetDice(dices);
        game.PlayerRollDice();
        game.PlayerMoveChess();

        // Act
        game.PayToll();

        // Assert
        Assert.AreEqual(1050, a.Money);
        Assert.AreEqual(950, b.Money);
    }
    [TestMethod]
    [Description(
    """
        Given:  玩家A, B
                玩家A持有的金額 1000, 房地產 A1 A4, A4地價 1000, A4有2棟房子
                玩家B持有的金額 2000
                B的回合移動到A4
        When:   B扣除過路費1000 * 100% * 130% = 1300給A
        Then:   玩家A持有金額為1000+1300 = 2300
                玩家A持有金額為2000-1300 = 700
        """)]
    public void 玩家付過路費_有2房_有1同地段()
    {
        // Arrange
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game("T02", map);

        Player a = new Player("A", 1000);
        Player b = new Player("B", 2000);
        game.AddPlayers(a, b);

        game.CurrentPlayer = a;
        var A1 = map.FindBlockById("A1");
        A1.Contract.SetOwner(a);
        var A4 = map.FindBlockById("A4");
        A4.Contract.SetOwner(a);
        A4.Contract.Upgrade(2);

        game.SetPlayerToBlock(b, "A2", Direction.Right);
        var dices = Utils.MockDice(2);
        game.SetDice(dices);
        game.CurrentPlayer = b;
        game.PlayerRollDice();
        game.PlayerMoveChess();

        // Act
        game.PayToll();
        Assert.AreEqual(2300, a.Money);
        Assert.AreEqual(700, b.Money);
    }

    [TestMethod]
    [Description(
        """
        Given:  玩家A, B
                目前輪到A
                A在A1上，持有1000元
                B持有1000元
                B在監獄(Jail)
                A1是B的土地，價值1000元
        When:   A付過路費
        Then:   A無須付過路費
        """)]
    public void 地主在監獄無需付過路費()
    {
        // Arrange
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game("T09", map);
        Player a = new Player("A", 1000);
        Player b = new Player("B", 1000);
        game.AddPlayers(a, b);

        game.SetPlayerToBlock(a, "Station1", Direction.Left);
        game.SetPlayerToBlock(b, "Prison", Direction.Right);

        var A1 = map.FindBlockById("A1");
        A1.Contract.SetOwner(b);
        var dices = Utils.MockDice(1);
        game.SetDice(dices);
        game.CurrentPlayer = a;
        game.PlayerRollDice();
        game.PlayerMoveChess();

        // Act
        game.PayToll();

        // Assert
        Assert.AreEqual(1000, a.Money);
        Assert.AreEqual(1000, b.Money);
    }

    [TestMethod]
    [Description(
    """
        Given:  玩家A, B
                目前輪到A
                A在A1上，持有1000元
                B持有1000元
                B在停車場
                A1是B的土地，價值1000元
        When:   A付過路費
        Then:   A無須付過路費
        """)]
    public void 地主在停車場無需付過路費()
    {
        // Arrange
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game("T09", map);
        Player a = new Player("A", 1000);
        Player b = new Player("B", 1000);
        game.AddPlayers(a, b);

        game.SetPlayerToBlock(a, "Station1", Direction.Left);
        game.SetPlayerToBlock(b, "Prison", Direction.Right);

        var A1 = map.FindBlockById("A1");
        A1.Contract.SetOwner(b);
        var dices = Utils.MockDice(1);
        game.SetDice(dices);
        game.CurrentPlayer = a;
        game.PlayerRollDice();
        game.PlayerMoveChess();

        // Act
        game.PayToll();

        // Assert
        Assert.AreEqual(1000, a.Money);
        Assert.AreEqual(1000, b.Money);
    }
}

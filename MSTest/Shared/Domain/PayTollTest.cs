using Application.Domain;

namespace Test.Application.Domain;

[TestClass]
public class PayTollTest
{
	[TestMethod]
    [Description(
    """
    Given:  玩家A, B
            玩家A持有的金額 1000, 房地產 A4, 地價 1000, 沒有房子
            玩家B持有的金額 1000
            B的回合移動到A4
    When:   B扣除過路費1000 * 5% = 50給A
    Then:   玩家A持有金額為1000+50 = 1050
            玩家A持有金額為1000-50 = 950
    """)]
    public void 玩家付過路費_無房_無同地段()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map, Utils.MockDice(2));
        Player a = new("A", 1000),
                b = new("B", 1000);
        game.AddPlayers(a, b);

        game.Initial();
        game.CurrentPlayer = b;

        Land A4 = (Land)map.FindBlockById("A4");
        a.AddLandContracts(A4);

        game.SetPlayerToBlock(b, "A2", Direction.Right);
        game.PlayerRollDice(b.Id);

        Land location = (Land)game.GetPlayerPosition(b.Id);

        decimal amount = 0;
        Player? payee = game.GetOwner(location);

        bool enoughMoney = game.CalculateToll(location, b, payee!, out amount);
        Assert.AreEqual(true, enoughMoney);

        if (enoughMoney)
        {
            // Act
            game.PayToll(b, a, amount);
            Assert.AreEqual(1050, a.Money);
            Assert.AreEqual(950, b.Money);

        }
    }
    [TestMethod]
    [Description(
    """
    Given:  玩家A, B
            玩家A持有的金額 3000, 房地產 A1 A4, A4地價 1000, A4有2棟房子
            玩家B持有的金額 2000
            B的回合移動到A4
    When:   B扣除過路費1000 * 100% * 130% = 1300給A
    Then:   玩家A持有金額為1000+1300 = 2300
            玩家A持有金額為2000-1300 = 700
    """)]
    public void 玩家付過路費_有2房_有1同地段()
    {
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map, Utils.MockDice(2));
        Player a = new("A", 3000),
                b = new("B", 2000);
        game.AddPlayers(a, b);
        game.Initial();
        game.CurrentPlayer = b;

        Land A1 = (Land)map.FindBlockById("A1");
        Land A4 = (Land)map.FindBlockById("A4");
        a.AddLandContracts(A1, A4);
        game.UpgradeLand(A4, 2);

        game.SetPlayerToBlock(b, "A2", Direction.Right);
        game.PlayerRollDice(b.Id);

        Land location = (Land)game.GetPlayerPosition(b.Id);

        decimal amount = 0;
        Player? payee = game.GetOwner(location);

        bool enoughMoney = game.CalculateToll(location, b, payee!, out amount);
        Assert.AreEqual(true, enoughMoney);

        if (enoughMoney)
        {
            game.PayToll(b, a, amount);

            Assert.AreEqual(2300, a.Money);
            Assert.AreEqual(700, b.Money);
        }
    }

    [TestMethod]
    [Description(
    """
    Given:  玩家A, B
            目前輪到A
            A在Start上，持有1000元
            B持有1000元
            B在監獄(Prison)
            A1是B的土地，價值1000元
    When:   A付過路費
    Then:   A無須付過路費
    """)]
    public void 地主在監獄無需付過路費()
    {
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map, Utils.MockDice(1));

        Player a = new("A"),
                b = new("B");

        game.AddPlayers(a, b);
        game.Initial();

        game.SetPlayerToBlock(a, "Start", Direction.Right);
        Land A1 = (Land)map.FindBlockById("A1");
        b.AddLandContracts(A1);

        game.SetPlayerToBlock(b, "Jail", Direction.Left);
        game.PlayerRollDice(a.Id);

        Land location = (Land)game.GetPlayerPosition(a.Id);
        decimal amount = 0;
        Player? payee = game.GetOwner(location);

        bool enoughMoney = game.CalculateToll(location, b, payee!, out amount);
        Assert.AreEqual(false, enoughMoney);
        Assert.AreEqual(0, amount);
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
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map, Utils.MockDice(1));

        Player a = new("A", 1000),
                b = new("B", 1000);

        game.AddPlayers(a, b);
        game.Initial();

        Land A1 = (Land)map.FindBlockById("A1");
        b.AddLandContracts(A1);
        game.SetPlayerToBlock(b, "ParkingLot", Direction.Left);
        game.SetPlayerToBlock(a, "Start", Direction.Right);
        game.PlayerRollDice(a.Id);
            
        Land location = (Land)game.GetPlayerPosition(a.Id);
        decimal amount = 0;
        Player? payee = game.GetOwner(location);

        bool enoughMoney = game.CalculateToll(location, b, payee!, out amount);
        Assert.AreEqual(false, enoughMoney);
        Assert.AreEqual(0, amount);
    }
}

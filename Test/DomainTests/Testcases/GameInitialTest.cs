using Domain;
using Domain.Builders;
using Domain.Maps;

namespace Test.DomainTests.Testcases;

[TestClass]
public class GameInitialTest
{
    private static Map Map => new SevenXSevenMap();

    [TestMethod]
    public void 玩家ABCD__遊戲初始設定__每位玩家有15000_每位玩家都會在起點()
    {
        // Arrange
        var a = new Player("A");
        var b = new Player("B");
        var c = new Player("C");
        var d = new Player("D");

        var map = Map;
        var monopoly = new MonopolyBuilder()
            .WithMap(Map)
            .WithPlayer(a.Id)
            .WithPlayer(b.Id)
            .WithPlayer(c.Id)
            .WithPlayer(d.Id)
            .WithCurrentPlayer(a.Id)
            .Build();

        // Act
        monopoly.Initial();

        // Assert
        var player_a = monopoly.Players.First(p => p.Id == a.Id);
        var player_b = monopoly.Players.First(p => p.Id == b.Id);
        var player_c = monopoly.Players.First(p => p.Id == c.Id);
        var player_d = monopoly.Players.First(p => p.Id == d.Id);
        Assert.AreEqual(15000, player_a.Money);
        Assert.AreEqual(15000, player_b.Money);
        Assert.AreEqual(15000, player_c.Money);
        Assert.AreEqual(15000, player_d.Money);

        Assert.AreEqual("Start", monopoly.GetPlayerPosition("A").Id);
        Assert.AreEqual("Start", monopoly.GetPlayerPosition("B").Id);
        Assert.AreEqual("Start", monopoly.GetPlayerPosition("C").Id);
        Assert.AreEqual("Start", monopoly.GetPlayerPosition("D").Id);
    }
}
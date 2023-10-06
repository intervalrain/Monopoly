using Application.Domain;

namespace Test.Application.Domain;

[TestClass]
public class GameTest
{
    [TestMethod]
    public void 玩家ABC_玩家BC破產__當遊戲結算__名次為ACB()
    {
        // Arrange
        Game game = new("Test");
        // 玩家 A B C
        var player_a = new Player("A");
        var player_b = new Player("B", 0);
        var player_c = new Player("C", 0);
        game.AddPlayer(player_a);
        game.AddPlayer(player_b);
        game.AddPlayer(player_c);

        // 玩家 B、C 破產
        game.UpdatePlayerState(player_b);
        game.UpdatePlayerState(player_c);

        // Act
        // 遊戲結算
        game.Settlement();

        // Assert
        // 玩家A獲勝
        Assert.AreEqual(1, game.PlayerRankDictionary[player_a]);
        Assert.AreEqual(3, game.PlayerRankDictionary[player_b]);
        Assert.AreEqual(2, game.PlayerRankDictionary[player_c]);

    }

    [TestMethod]
    public void 玩家ABCD_遊戲時間結束_A的結算金額為5000_B的結算金額為4000_C的結算金額為3000_D的結算金額為2000__當遊戲結算__名次為ABCD()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new("Test", map);
        Player a = new("A", 5000),
               b = new("B", 4000),
               c = new("C", 3000),
               d = new("D", 2000);
        game.AddPlayers(a, b, c, d);

        // Act
        game.Settlement();
        
        // Assert
        var ranking = game.PlayerRankDictionary;
        Assert.AreEqual(1, ranking[a]);
        Assert.AreEqual(2, ranking[b]);
        Assert.AreEqual(3, ranking[c]);
        Assert.AreEqual(4, ranking[d]);
    }
}
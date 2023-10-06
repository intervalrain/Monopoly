using Application.Domain;
using Application.Domain.Exceptions;

namespace Test.Application.Domain;

[TestClass]
public class SelectDirectionTest
{
    [TestMethod]
    [Description(
        """
        Given:  玩家A目前在停車場
                玩家A方向為Down
                玩家A需要選擇方向
        When:   玩家A選擇方向為Left
        Then:   玩家A在停車場
                玩家A方向為Left
        """)]
    public void 玩家選擇方向()
    {
        // Arrange
        var map = new Map(Utils.SevenXSevenMap);
        var game = new Game("Test", map);
        var player = new Player("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "ParkingLot", Direction.Down);

        // Act
        game.PlayerSelectDirection(player, Direction.Left);

        // Assert
        Assert.AreEqual("ParkingLot", game.GetPlayerPosition("A").Id);

        var direction = game.GetPlayerDirection("A");
        Assert.AreEqual(Direction.Left, direction);
    }

    [TestMethod]
    [Description(
        """
        Given:  玩家A目前在停車場
                玩家A方向為Down
                玩家A目前還能走3步
                玩家A需要選擇方向
        When:   玩家A選擇方向為Left
        Then:   玩家A在B4
                玩家A方向為Down
        """)]
    public void 玩家選擇方向後會繼續前進到定點()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map);
        Player player = new("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "ParkingLot", Direction.Down);
        Chess chess = game.CurrentPlayer!.Chess;
        game.CurrentPlayer.Chess = new Chess(player, map, chess.CurrentBlock, chess.CurrentDirection, 3);

        // Act
        game.PlayerSelectDirection(player, Direction.Left);

        // Assert
        Assert.AreEqual("B4", game.GetPlayerPosition("A").Id);
        var direction = game.GetPlayerDirection("A");
        Assert.AreEqual(Direction.Down, direction);
    }


    [TestMethod]
    [Description(
        """
        Given:  玩家A目前在停車場
                玩家A方向為Down
                玩家A目前還能走4步
                玩家A需要選擇方向
        When:   玩家A選擇方向為Left
        Then:   玩家A在監獄
                玩家A需要選擇方向
        """)]
    public void 玩家選擇方向後會繼續前進到需要選擇方向的地方()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Game game = new Game("Test", map);
        Player player = new Player("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "ParkingLot", Direction.Down);
        Chess chess = game.CurrentPlayer!.Chess;
        game.CurrentPlayer.Chess = new Chess(player, map, chess.CurrentBlock, chess.CurrentDirection, 4);

        // Act
        Assert.ThrowsException<PlayerNeedToChooseDirectionException>(() =>
            game.PlayerSelectDirection(player, Direction.Left));

        // Assert
        Assert.AreEqual("Jail", game.GetPlayerPosition("A").Id);
    }

}
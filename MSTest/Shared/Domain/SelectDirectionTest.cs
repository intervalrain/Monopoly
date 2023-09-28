using Shared.Domain;

namespace Test.Shared.Domain;

[TestClass]
public class SelectDirectionTest
{
    [TestMethod]
    public void 玩家A目前在停車場_方向為Down__選擇方向Left__玩家A在停車場_方向為Left()
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


}
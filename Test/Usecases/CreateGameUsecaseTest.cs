using Domain;
using Domain.Maps;
using Domain.Usecases;
using Server.Repositories;

namespace Test.Usecases;

[TestClass]
public class CreateGameUsecaseTest
{
    [TestMethod]
    public void 初始化遊戲_共四人遊戲()
    {
        // Arrange
        const string GameId = "g1";
        string[] PlayerIds = new[] { "p1", "p2", "p3", "p4" };

        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(GameId, map);
        CreateGameUsecase.Input input = new(GameId, PlayerIds);
        var presenter = new CreateGameUsecase.Presenter();

        // Act
        var usecase = new CreateGameUsecase(new InMemoryRepository());
        usecase.Execute(input, presenter);

        // Assert
        Assert.AreEqual("g1", presenter.GameId);
        Assert.AreEqual(4, presenter.PlayerNum);

    }
}

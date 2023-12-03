using Shared.Domain;
using Shared.Usecases;
using Server.Repositories;

namespace Test.Usecases;

[TestClass]
public class RollDiceUsecaseTest
{
    [TestMethod]
    public void 輪到玩家A_玩家A在起點_玩家擲骰子_沒有拋錯()
    {
        // Arrange
        const string GameId = "g1";
        const string PlayerId = "p1";

        var repo = new InMemoryRepository();
        new CreateGameUsecase(repo).Execute(
            new CreateGameUsecase.Input(GameId, new[] { PlayerId }),
            new CreateGameUsecase.Presenter());
        Game game = repo.FindGameById(GameId);
        repo.Save(game);

        RollDiceUsecase.Input input = new(GameId, PlayerId);
        var presenter = new RollDiceUsecase.Presenter();

        // Act
        var usecase = new RollDiceUsecase(repo);

        usecase.Execute(input, presenter);
    }
}

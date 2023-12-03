using Shared.Domain;
using Shared.Domain.Enums;
using Shared.Usecases;
using Server.Repositories;

namespace Test.Usecases;

[TestClass]
public class MoveChessUsecaseTest
{
	[TestMethod]
	[Description(
		"""
		Given: 輪到 A，A在起點
		 When: 擲骰子得到 5
		 Then: A 移動到 A4
		""")]
	public void 輪到A_A在起點_擲骰子得到5_移動棋子_棋子會在A4()
	{
		// Arrange
		const string GameId = "g1";
		const string PlayerId = "p1";

		var repo = new InMemoryRepository();
		new CreateGameUsecase(repo).Execute(
			new CreateGameUsecase.Input(GameId, new[] {PlayerId}),
			new CreateGameUsecase.Presenter());
		Game game = repo.FindGameById(GameId);
		game.SetDice(5);
		Player player = game.GetPlayerById(PlayerId);
		game.SetPlayerToBlock(player, "Start", Direction.Up);
		repo.Save(game);

		MoveChessUsecase.Input input = new(GameId, PlayerId);
		var presenter = new MoveChessUsecase.Presenter();

		// Act
		var usecase = new MoveChessUsecase(repo);
        
        usecase.Execute(input, presenter);

		// Assert
		Assert.AreEqual("A4", presenter.ChessPosition);
	}
}

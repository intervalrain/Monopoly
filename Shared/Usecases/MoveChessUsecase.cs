using Domain.Repositories;

namespace Domain.Usecases;

public class MoveChessUsecase
{
	private readonly IRepository _repository;

	public MoveChessUsecase(IRepository repository)
	{
		_repository = repository;
	}

	public void Execute(Input input, Presenter presenter)
	{
		// 查
		var game = _repository.FindGameById(input.GameId);
		// 改
		game.PlayerMoveChess();
		// 存
		_repository.Save(game);
		// 推
		presenter.ChessPosition = game.GetPlayerPosition(input.PlayerId).Id;
	}

	public record Input(string GameId, string PlayerId);

	public class Presenter
	{
		public string ChessPosition { get; set; }
	}

}

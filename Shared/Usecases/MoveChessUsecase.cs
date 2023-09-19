using System;
using Shared.Repositories;

namespace Shared.Usecases
{
	public class MoveChessUsecase
	{
		private readonly IRepository _repository; 
		public MoveChessUsecase(IRepository repo)
		{
			_repository = repo;
		}

		public void Execute(Input input, Presenter presenter)
		{
			// Find
			var game = _repository.FindGameById(input.GameId);
			// Update
			game.PlayerMoveChess(input.PlayerId);
			// Save
			_repository.Save(game);
			// Push
			presenter.ChessPosition = game.GetPlayerPosition(input.PlayerId);
		}

		public record Input(string GameId, string PlayerId);

		public class Presenter
		{
			public Presenter()
			{
			}
			public string ChessPosition { get; set; }
		}
	}
}


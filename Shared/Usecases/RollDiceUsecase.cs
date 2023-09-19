using System;
using Shared.Repositories;

namespace Shared.Usecases
{
	public class RollDiceUsecase
	{
		private readonly IRepository _repository;

		public RollDiceUsecase(IRepository repo)
		{
			_repository = repo;
		}

		public void Execute(Input input, Presenter presenter)
		{
			// Find
			var game = _repository.FindGameById(input.GameId);
			// Update
			game.PlayerRollDice(input.PlayerId);
			// Save
			_repository.Save(game);
			// Push
			presenter.Dice = game.CurrentDice; ;
		}

		public class Presenter
		{
			public int Dice { get; set; }
		}

		public record Input(string GameId, string PlayerId);
	}
}


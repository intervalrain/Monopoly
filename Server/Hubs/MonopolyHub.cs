using Application.Usecases;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs;

public class MonopolyHub : Hub<IMonopolyResponse>
{
	public async Task CreateGame(string gameId, string userId, CreateGameUsecase usecase)
	{
		await usecase.ExecuteAsync(new CreateGameRequest(gameId, userId));
	}

	public async Task PlayerRollDice(string gameId, string userId, RollDiceUsecase usecase)
	{
		await usecase.ExecuteAsync(new RollDiceRequest(gameId, userId));
	}
}

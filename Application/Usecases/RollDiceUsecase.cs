using Application.Common;
using Domain;
using Domain.Common;

namespace Application.Usecases;

public record RollDiceRequest(string GameId, string PlayerId)
    : Request(GameId, PlayerId);

public class RollDiceUsecase : Usecase<RollDiceRequest>
{
    public RollDiceUsecase(IRepository repository, IEventBus<DomainEvent> eventBus)
        : base(repository, eventBus)
    {
    }

    public override async Task ExecuteAsync(RollDiceRequest request)
    {
        // 查
        Game game = Repository.FindGameById(request.GameId);

        // 改
        game.PlayerRollDice();

        // 存
        Repository.Save(game);

        // 推
        await EventBus.PublishAsync(game.DomainEvents);
    }
}
using Application.Domain.Common;

namespace Application.Domain.Events;

public record PlayerRollDiceEvent(string GameId, string PlayerId, int DiceCount)
    : DomainEvent(GameId);
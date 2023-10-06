using Application.Domain.Common;

namespace Application.Domain.Events;

public record ChessMoveEvent(string GameId, string PlayerId, string BlockId)
    : DomainEvent(GameId);
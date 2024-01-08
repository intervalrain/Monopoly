using Domain.Common;

namespace Domain.Events;

public record ChessMovedEvent(string PlayerId, string BlockId, Direction Direction, int RemainingSteps) : DomainEvent;
using Domain.Common;

namespace Domain.Events;

public record ChessMoveEvent(string PlayerId, string BlockId, string Direction, int RemainingSteps) : DomainEvent;
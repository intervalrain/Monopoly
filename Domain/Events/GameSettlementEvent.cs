using Domain.Common;

namespace Domain.Events;

public record GameSettleEvent(int Rounds, params Player[] Players) : DomainEvent;
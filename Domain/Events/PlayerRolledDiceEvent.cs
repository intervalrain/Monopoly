using Domain.Common;

namespace Domain.Events;

public record PlayerRolledDiceEvent(string PlyaerId, int DiceCount) : DomainEvent;
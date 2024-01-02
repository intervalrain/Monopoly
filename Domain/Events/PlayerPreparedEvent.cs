using Domain.Common;

namespace Domain.Events;

public record PlayerReadEvent(string PlayerId, string PlayerState) : DomainEvent;
public record PlayerCannotReadyEvent(string PlayerId, string PlayerState, string? RoleId, int LocationId) : DomainEvent; 
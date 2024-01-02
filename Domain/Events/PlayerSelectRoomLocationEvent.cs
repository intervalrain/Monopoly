using Domain.Common;

namespace Domain.Events;

public record PlayerSelectLocationEvent(string PlayerId, int LocationId) : DomainEvent;
public record PlayCannotSelectLocationEvent(string PlayerId, int LocationId) : DomainEvent;
using Application.Domain.Common;

namespace Application.Domain.Events;

public record GameCreatedEvent(string GameId) : DomainEvent(GameId);
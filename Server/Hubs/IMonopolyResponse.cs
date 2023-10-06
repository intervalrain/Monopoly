using Application.Domain.Common;

namespace Server.Hubs;

public interface IMonopolyResponse
{
    Task GameCreatedEvent(DomainEvent domainEvent);
}
using Application.Common;
using Domain.Common;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs;

namespace Server;

public class MonopolyEventBus : IEventBus<DomainEvent>
{
	private readonly IHubContext<MonopolyHub, IMonopolyResponse> _hubContext; 

	public MonopolyEventBus(IHubContext<MonopolyHub, IMonopolyResponse> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task PublishAsync(IEnumerable<DomainEvent> events)
	{
		await _hubContext.Clients.All.GameCreatedEvent(events.First());
	}
}

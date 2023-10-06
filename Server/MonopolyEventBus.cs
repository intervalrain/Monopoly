using Application.Common;
using Application.Domain.Common;
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
        // 不同事件，推送給不同的客戶端
        // 若事件的內容物相同，SignalR會認為是同一種型態(跨網路傳輸的資料，不會有型態的資訊)
        await _hubContext.Clients.All.GameCreatedEvent(events.First());
    }
}
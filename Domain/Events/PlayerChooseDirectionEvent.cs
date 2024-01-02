using Domain.Common;

namespace Domain.Events;

public record PlayerCanChooseDirectionEvent(string PlayerId, string Direction) : DomainEvent; 
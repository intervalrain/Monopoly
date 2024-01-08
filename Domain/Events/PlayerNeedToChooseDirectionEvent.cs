using Domain.Common;

namespace Domain.Events;

public record PlayerNeedToChooseDirectionEvent(string PlayerId, params Direction[] Directions) : DomainEvent;

using Domain.Common;

namespace Domain.Events;

public record PlayerNeedsToPayTollEvent(string PlayerId, string OwnerId, decimal Toll) : DomainEvent;
public record PlayerPayTollEvent(string PlayerId, decimal PlayerMoney, string OnwerId, decimal OwnerMoney) : DomainEvent;
public record PlayerDoesntNeedToPayTollEvent(string PlayerId, decimal PlayerMoney) : DomainEvent;
public record PlayerTooPoorToPayTollEvent(string PlayerId, decimal PlayerMoney, decimal Toll) : DomainEvent;
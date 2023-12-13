namespace Domain.Common;

public class AbstractAggregateRoot
{
	private readonly List<DomainEvent> domainEvents;

	public IReadOnlyList<DomainEvent> DomainEvents => domainEvents.AsReadOnly();

	public AbstractAggregateRoot()
	{
		domainEvents = new List<DomainEvent>();
	}

	public void AddDomainEvent(DomainEvent domainEvent)
	{
		domainEvents.Add(domainEvent);
	}

	public void ClearEvent()
	{
		domainEvents.Clear();
	}
}

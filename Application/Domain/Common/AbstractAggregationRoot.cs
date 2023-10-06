namespace Application.Domain.Common;

public abstract class AbstractAggregationRoot
{
	private readonly List<DomainEvent> domainEvents;

	public IReadOnlyList<DomainEvent> DomainEvents => domainEvents.AsReadOnly();

	protected AbstractAggregationRoot()
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

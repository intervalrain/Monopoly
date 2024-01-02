using Domain.Common;

namespace Domain.Events;

public record GameStartEvent(string GameStage, string CurrentPlayerId) : DomainEvent;
public record OnlyOnePersonEvent(string GameStage) : DomainEvent;
public record SomePlayerPreparingEvent(string GameStage, params string[] PlayerIds) : DomainEvent;
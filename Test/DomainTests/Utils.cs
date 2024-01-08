using Domain.Common;
using Domain.Events;
using Domain.Interfaces;
using Moq;

namespace Test.DomainTests;

public class Utils
{
	public static IDice[] MockDice(params int[] diceValues)
	{
		var dice = new IDice[diceValues.Length];
		for (int i = 0; i < diceValues.Length; i++)
		{
			var mockDice = new Mock<IDice>();
			mockDice.Setup(x => x.Roll());
			mockDice.Setup(x => x.Value).Returns(diceValues[i]);
			dice[i] = mockDice.Object;
		}
		return dice;
	}
}

public static class DomainEventExtension
{
	public static IEnumerable<DomainEvent> NextShouldBe(this IEnumerable<DomainEvent> domainEvents, DomainEvent e)
	{
		var first = domainEvents.First();
		if (first is PlayerNeedToChooseDirectionEvent e1)
		{
			var (PlayerId, Directions) = ((e1).PlayerId, ((PlayerNeedToChooseDirectionEvent)e).Directions);
			Assert.AreEqual(PlayerId, e1.PlayerId);
			CollectionAssert.AreEquivalent(Directions, e1.Directions);
		}
		else if (first is GameSettlementEvent e2)
		{
			var (Rounds, Players) = (((GameSettlementEvent)e).Rounds, ((GameSettlementEvent)e).Players);
			Assert.AreEqual(Rounds, e2.Rounds);
			CollectionAssert.AreEqual(Players, e2.Players);
		}
		else if (first is SomePlayerPreparingEvent e3)
		{
			var (GameStage, Players) = (((SomePlayerPreparingEvent)e).GameStage, ((SomePlayerPreparingEvent)e).PlayerIds);
			Assert.AreEqual(GameStage, e3.GameStage);
			CollectionAssert.AreEqual(Players, e3.PlayerIds);
		}
		else
		{
			Assert.AreEqual(e, first);
		}
		return domainEvents.Skip(1);
	}

	public static void NoMore(this IEnumerable<DomainEvent> domainEvents)
	{
		Assert.IsFalse(domainEvents.Any(), string.Join('\n', domainEvents));
	}
}

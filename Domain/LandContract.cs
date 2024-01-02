using Domain.Models;
using Domain.Common;
using Domain.Events;

namespace Domain;

public class LandContract
{
	public int DeadLine { get; private set; }
	public bool InMortgage { get; private set; } = false;
	public Player? Owner { get; set; }
	public Land Land { get; }
	public int House => Land.House;

	public LandContract(Player? owner, Land land)
	{
		Owner = owner;
		Land = land;
	}

	internal DomainEvent EndRound()
	{
		if (InMortgage)
		{
			if (--DeadLine == 0)
			{
				Land.UpdateOwner(null);
				return new MortgageDueEvent(Owner.Id, Land.Id);
			}
			else
			{
				return new MortgageCountdownEvent(Owner.Id, Land.Id, DeadLine);
			}
		}
		return DomainEvent.EmptyEvent;
	}

	internal void GetMortgage()
	{
		DeadLine = 10;
		InMortgage = true;
	}

	internal void GetRedeem()
	{
		InMortgage = false;
	}

	public void SetDeadLine(int deadLine)
	{
		DeadLine = deadLine;
	}
}

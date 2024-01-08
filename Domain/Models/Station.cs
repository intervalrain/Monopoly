using Domain.Common;
using Domain.Events;

namespace Domain.Models;

public class Station : Land
{
	public Station(string id, decimal price = 1000, string lot = "S") : base(id, lot, price)
	{
	}

    internal override DomainEvent OnBlockEvent(Player player)
    {
        Player? owner = GetOwner();
        var land = this;
        if (owner is null)
        {
            return new PlayerCanBuyLandEvent(player.Id, land.Id, land.Price);
        }
        else if (owner!.SuspendRounds <= 0)
        {
            DoBlockAction(player);
            return new PlayerNeedsToPayTollEvent(player.Id, owner.Id, land.CalculateToll(owner));
        }
        return DomainEvent.EmptyEvent;
    }

    internal override void DoBlockAction(Player player)
    {
        Player? owner = GetOwner();
        if (owner is null)
        {
            return;
        }
        if (owner!.SuspendRounds <= 0)
        {
            player.EndRoundFlag = false;
        }
    }
}


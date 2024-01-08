using Domain.Common;

namespace Domain.Models;

public class ParkingLot : Block
{
	public ParkingLot(string id) : base(id)
	{
	}

    internal override DomainEvent OnBlockEvent(Player player)
    {
        return DomainEvent.EmptyEvent;
    }

    internal override void DoBlockAction(Player player)
    {
        player.SuspendRound(this);
    }
}


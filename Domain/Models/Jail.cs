using Domain.Common;
using Domain.Events;

namespace Domain.Models;

public class Jail : Block
{
	public Jail(string id) : base(id)
	{
	}

    internal override DomainEvent OnBlockEvent(Player player)
    {
        return new SuspendRoundEvent(player.Id, player.SuspendRounds);
    }

    internal override void DoBlockAction(Player player)
    {
        player.SuspendRound(this);
    }
}


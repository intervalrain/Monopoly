using Domain.Common;
using Domain.Events;

namespace Domain.Models;

public class StartPoint : Block
{
	public StartPoint(string id) : base(id)
	{
	}

    internal override DomainEvent OnBlockEvent(Player player)
    {
        return new CannotGetRewardBecauseStandOnStartEvent(player.Id, player.Money);
    }

    internal override void DoBlockAction(Player player)
    {
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain;

public class Game
{
	private Dictionary<string, Player> _players = new();
	private List<Player> _rank = new();

	public void AddPlayer(Player player)
	{
		_players.Add(player.Id, player);
	}

    public void AddPlayers(params Player[] players)
    {
		foreach (var player in players)
			_players.Add(player.Id, player);
    }

	public void AllocateMoney(Player? player, int money)
	{
		if (player == null) return;
		if (player.AddMoney(money))
		{
			if (!_rank.Contains(player))
			{
                _rank.Add(player);
            }
		}
	}

	public List<Player> Settlement()
	{
		var left = _players.Where(p => p.Value.State == PlayerState.Normal)
						   .Select(p => p.Value)
						   .OrderBy(p => p.Money);
		foreach (var player in left)
		{
			_rank.Add(player);
		}
		_rank.Reverse();
		return _rank;
	}
}

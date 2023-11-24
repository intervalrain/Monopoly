using Shared.Domain.Enums;
using Shared.Interfaces;

namespace Shared.Domain;

public class Game
{
	private Dictionary<string, Player> _players = new();
	private List<Player> _rank = new();
	private Map? _map; 

	public Game(Map? map = null)
	{
		_map = map;
	}

	public void AddPlayer(Player player)
	{
		if (_players.ContainsKey(player.Id))
			throw new InvalidOperationException("Duplicated player's name");
		_players.Add(player.Id, player);
		if (_map != null)
		{
            var start = _map.FindBlockById("Start");
            player.Init(start, Direction.Down);
        }
	}

    public void AddPlayers(params Player[] players)
    {
		foreach (var player in players)
			AddPlayer(player);
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

	public IBlock GetPlayerPosition(Player player)
	{
		if (!_players.ContainsKey(player.Id))
			throw new KeyNotFoundException(player.Id);
		return player.Position;
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

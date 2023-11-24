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
            player.SetPosition(start, Direction.Down);
        }
	}

    public void AddPlayers(params Player[] players)
    {
		foreach (var player in players)
			AddPlayer(player);
    }

	public void SetPlayerToBlock(Player player, string blockId, Direction direction)
	{
		player.SetPosition(_map!.FindBlockById(blockId), direction);
	}

	public void MovePlayer(Player player, int moves)
	{
		player.Move(moves);
	}

	public void PlayerBuyLand(Player player, IBlock block)
	{
		player.BuyLand(block);
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
		return player.Position!;
	}

	public List<Player> Settlement()
	{
		if (_map != null)
		{
            foreach (var block in _map!.Blocks)
            {
                var contract = block.Contract;
                if (contract.Owner == null) continue;
                var owner = contract.Owner;
                owner.AddMoney(contract.Value);
                contract.SetOwner(null);
            }
        }
		
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

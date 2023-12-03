using Shared.Domain.Enums;
using Shared.Domain.Maps;
using Shared.Interfaces;

namespace Shared.Domain;

public class Game
{
	private Dictionary<string, Player> _players = new();
	private List<Player> _rank = new();
	private Map? _map;
	private string _id;
	private IDice[] _dices;
	
	public List<Player> Players => _players.Select(p => p.Value).ToList();
	public List<Player> Rank => _rank; 
	public string Id => _id;
	public int CurrentDice { get; set; }
	public Player CurrentPlayer { get; set; }
	public IDice[] Dices => _dices;

	public Game(string id, Map? map = null)
	{
		_id = id;
		_map = map;
	}

    public static Game Create(string gameId, params string[] playerIds)
    {
		Map map = new Map(_7x7Map.Standard7x7); 
        Game game = new Game(gameId, map);
		game.AddPlayers(playerIds.Select(p => new Player(p)).ToArray());
		return game;
    }

    public void AddPlayer(Player player)
	{
		if (_players.ContainsKey(player.Id))
			throw new InvalidOperationException("Duplicated player's name");
		_players.Add(player.Id, player);
		if (CurrentPlayer == null) CurrentPlayer = player;
		if (_map != null && player.Position == null)
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

	public void PlayerAuctionLand(Player player, IBlock block)
	{
		player.SellLand(block);
	}

	public void PlayerSellLand(Player buyee, Player buyer, IBlock block)
	{
		if (buyer.Money < block.Contract.Value) return;
		buyee.SellLand(block);
		buyer.BuyLand(block);
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
		return player.Position!;
	}

	public IBlock GetPlayerPosition(string playerId)
	{
		return GetPlayerPosition(GetPlayerById(playerId));
	}

	public Player GetPlayerById(string playerId)
	{
		if (!_players.ContainsKey(playerId))
			throw new KeyNotFoundException(playerId);
		return _players[playerId];
	}

	public void PlayerMoveChess()
	{
		Player player = CurrentPlayer;
		player.Move(CurrentDice);
	}

	public void SetDice(IDice[]? dices = null)
	{
		_dices = dices;
	}

	public void PlayerRollDice(string playerId)
	{
		var player = GetPlayerById(playerId);
		CurrentDice = player.RollDice(Dices);
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

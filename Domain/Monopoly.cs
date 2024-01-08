using Domain.Common;
using Domain.Interfaces;
using Domain.Events;
using Domain.Builders;

namespace Domain;

public class Monopoly : AbstractAggregationRoot
{
	private readonly Map _map;
	private readonly List<Player> _players = new();
	private Player _currentPlayer => _players.First(p => p.Id == _currentPlayerState.PlayerId);
	private CurrentPlayerState _currentPlayerState;
	
	public string Id { get; set; }
	public GameStage GameStage { get; private set; }
	public int[]? CurrentDice { get; set; } = null;
	public Player CurrentPlayer => _currentPlayer; 
	public CurrentPlayerState CurrentPlayerState => _currentPlayerState;
	public IDice[] Dices { get; set; }
	public ICollection<Player> Players => _players.AsReadOnly();
	public string HostId { get; init; }
	public Map Map => _map;
	public int Rounds { get; private set; }

	public Monopoly(string gameId, Player[] players, GameStage gameStage, Map map, string hostId, CurrentPlayerState currentPlayerState, IDice[]? dices, int rounds = 0)
	{
		Id = gameId;
		GameStage = gameStage;
		_players = players.ToList();
		_map = map;
		HostId = hostId;
		_currentPlayerState = currentPlayerState;
		Dices = dices ?? new IDice[2] { new Dice(), new Dice() };
		Rounds = rounds;
	}

	public void AddPlayer(Player player, string blockId = "Start", Direction  direction  = Direction.Right)
	{
		Chess chess = new(player, blockId, direction, 0);
		player.Chess = chess;
		_players.Add(player);
	}

	public void Settlement()
	{
		var PropertyCalculate = (Player player) =>
			player.Money + player.LandContractList.Where(l => !l.InMortgage).Sum(l => (l.Land.House + 1) * l.Land.Price);
		var players = _players.OrderByDescending(PropertyCalculate).ThenByDescending(p => p.BankruptRounds).ToArray();
		AddDomainEvent(new GameSettlementEvent(Rounds, players));
	}

	public Block GetPlayerPosition(string playerId)
	{
		Player player = GetPlayer(playerId);
		return _map.FindBlockById(player.Chess.CurrentBlockId);
	}

	public void Initial()
	{
		_currentPlayerState = new CurrentPlayerStateBuilder(_players[0].Id).Build(null);
		CurrentPlayer.StartRound();
	}

	public void PlayerRollDice(string playerId)
	{
		Player player = GetPlayer(playerId);
		VerifyCurrentPlayer(player);
		var events = player.RollDice(_map, Dices).ToArray();
		this.AddDomainEvents(events);
	}

    #region Private Functions 
    private Player GetPlayer(string id)
	{
		var player = _players.Find(p => p.Id == id) ?? throw new Exception("找不到玩家");
		return player; 
	}

	private void VerifyCurrentPlayer(Player? player)
	{
		if (player != CurrentPlayer)
		{
			throw new Exception("不是該玩家的回合");
		}
	}
    #endregion
}

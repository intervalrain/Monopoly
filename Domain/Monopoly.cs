using Domain.Common;
using Domain.Interfaces;

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
		Dices = dices ?? new IDices[2] { new Dice(), new Dice() };
		Rounds = rounds;
	}
}

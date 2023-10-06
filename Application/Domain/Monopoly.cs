using Application.Domain.Common;
using Application.Domain.Interfaces;

namespace Application.Domain;

public class Monopoly : AbstractAggregationRoot
{
	public string Id { get; set; }
	public int[]? CurrentDice { get; set; } = null;
	public Player? CurrentPlayer { get; set; }
	public IDice[] Dices { get; init; }

	private readonly Map _map;
	private readonly List<Player> _players = new();
	private readonly Dictionary<Player, int> _playerRankDictionary = new();

	public IDictionary<Player, int> PlayerRankDictionary => _playerRankDictionary.AsReadOnly(); 

	public Monopoly()
	{
	}
}

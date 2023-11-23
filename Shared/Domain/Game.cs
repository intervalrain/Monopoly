using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain;

public class Game
{
	private Dictionary<string, Player> players = new(); 

	public void AddPlayer(string id)
	{
		players.Add(id, new Player(id));
	}

    public void AddPlayers(params string[] ids)
    {
		foreach (string id in ids)
			players.Add(id, new Player(id));
    }

    public void SetState(string id, PlayerState playerState)
	{
		var player = FindPlayerById(id);
		if (player != null)
			player.State = PlayerState.Bankrupt;
	}

	public Player? FindPlayerById(string id)
	{
		if (!players.ContainsKey(id)) throw new KeyNotFoundException();
		return players[id];
	}

	public Player? Settlement()
	{
		var winner = players.Where(p => p.Value.State == PlayerState.Normal);
		if (winner.Count() != 1) throw new Exception("Game not end");
		return winner.FirstOrDefault().Value;
	}
}

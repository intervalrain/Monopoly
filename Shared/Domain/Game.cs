using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain
{
	public class Game
	{
		List<Player> players = new();

		public Dictionary<Player?, int> RankList { get; set; } = new();

		public void AddPlayer(string id)
		{
			players.Add(new Player(id));
        }

		public void SetState(string id, PlayerState state)
		{
			var player = FindPlayerById(id);
			if (player == null)
			{
				return;
			}
			player.SetState(state);

			if (state == PlayerState.Bankrupt)
			{
				AddPlayerToRankList(player);
			}
        }

		public void Settlement()
		{
			var playerList = from p in players
						   where !p.IsBankrupt()
						   orderby p.Moeny + p.LandContractList.Sum(l => l.Price + (l.House * l.Price)) ascending
						   select p;
			foreach (var player in playerList)
			{
                AddPlayerToRankList(player);
			}
		}

		private void AddPlayerToRankList(Player player)
		{
			foreach(var rank in RankList)
			{
				RankList[rank.Key] += 1;
			}
			RankList.Add(player, 1);
		}

		public Player? FindPlayerById(string id) => players.FirstOrDefault(p => p.Id == id);
	}
}


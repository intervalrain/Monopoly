using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain
{
	public class Game
	{
        private readonly List<Player> players = new();
        public Map Map { get; }
        public Dictionary<Player, int> RankList { get; set; } = new();

        public Game(Map? initMap = null)
		{
			Map = initMap ?? new Map(Array.Empty<IBlock[]>());
		}

        public void AddPlayer(Player player)
		{
			players.Add(player);
        }

        public void SetState(Player? player, PlayerState state)
        {
			if (player == null) return;
			AddPlayerToRankList(player);
			player.SetState(state);
        }

        public void Settlement()
		{
			var playerList = from p in players
							 where p.State != PlayerState.Bankrupt
							 orderby p.Money + p.LandContractList.Sum(l => l.Price + (l.Level * l.Price)) ascending
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
	}
}


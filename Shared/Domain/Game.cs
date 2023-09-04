using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain
{
	public class Game
	{
		private List<Player> players = new();

		public Dictionary<Player, int> RankList { get; set; } = new();

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
							 where !p.IsBankrupt()
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


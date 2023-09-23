using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Shared.Domain.Map;

namespace Shared.Domain
{
    public class Game
    {
        private readonly Map _map;
        private readonly List<Player> _players = new();
        private readonly Dictionary<Player, int> _rankMap = new();
        private string _id;
        private DiceSetting _diceSetting = new DiceSetting();

        public IDictionary<Player, int> RankMap => _rankMap.AsReadOnly();
        public int CurrentDice { get; set; }
        //public Player? CurrentPlayer { get; set; }
        public string CurrentPlayerId { get; set; }
        public string Id => _id;
        public List<Player> Players => _players;
        public DiceSetting DiceSetting => _diceSetting; 

        public Game(string id, Map? initMap = null, DiceSetting? diceSetting = null)
        {
            _id = id;
            _map = initMap ?? new Map(Array.Empty<Block[]>());
            _diceSetting = diceSetting ?? new DiceSetting();
        }

        public void AddPlayer(Player player) => _players.Add(player);

        public void AddPlayers(params Player[] players) => _players.AddRange(players);

        public void SetPlayerState(Player player, PlayerState state)
        {
            player.SetState(state);
            if (state == PlayerState.Bankrupt)
            {
                AddPlayerToRankList(player);
            }
        }

        public void Settlement()
        {
            var playerList = from p in _players
                             where p.State != PlayerState.Bankrupt
                             orderby p.Money + p.LandContractList.Sum(l => l.Value) ascending
                             select p;
            foreach (var player in playerList)
            {
                AddPlayerToRankList(player);
            }
        }

        public void SetPlayerToBlock(Player player, string blockId, Direction direction) => _map.SetPosition(player, blockId, direction);

        public void MovePlayer(Player player, int point) => _map.MovePlayer(player, point);

        public string GetPlayerPosition(Player player) => _map.GetPlayerPositionedBlockId(player);

        public string GetPlayerPosition(string playerId) => _map.GetPlayerPositionedBlockId(FindPlayerById(playerId));

        private void AddPlayerToRankList(Player player)
        {
            foreach (var rank in _rankMap)
            {
                _rankMap[rank.Key] += 1;
            }
            _rankMap.Add(player, 1);
        }

        public void UpdatePlayerState(Player player)
        {
            if (player.Money == 0 && player.LandContractList.Count == 0)
            {
                SetPlayerState(player, PlayerState.Bankrupt);
            }
        }

        public void Initial()
        {
            foreach (Player player in _players)
            {
                Direction d = (Direction)(new Random().Next(4));
                SetPlayerToBlock(player, "Start", d);
            }
            CurrentPlayerId = _players.First().Id;
        }

        public void SelectionDirection(Player player, Direction direction)
        {
            player.Direction = direction;
        }

        public Direction GetPlayerDirection(Player player) => player.Direction;

        public Direction GetPlayerDirection(string playerId) => FindPlayerById(playerId).Direction;

        public Player FindPlayerById(string playerId)
        {
            Player? player = _players.First(p => p.Id == playerId);
            if (player == null) throw new NullReferenceException($"{playerId} doesn't exist.");
            return player;
        }

        public void PlayerMoveChess(string playerId)
        {
            Player player = FindPlayerById(playerId);
            CurrentPlayerId = player.Id;
            MovePlayer(player, CurrentDice);

        }

        public void PlayerRollDice(string playerId)
        {
            Player player = FindPlayerById(playerId);
            CurrentPlayerId = player.Id;
            int dice = 0;
            for (int i = 0; i < _diceSetting.NumberOfDice; i++)
            {
                dice += new Random().Next(_diceSetting.Min, _diceSetting.Max);
            }
            CurrentDice = dice;
        }
    }
}


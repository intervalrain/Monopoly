using Shared.Domain.Interfaces;

namespace Shared.Domain;

public class Game
{
    public string Id { get; init; }
    public int[]? CurrentDice { get; set; } = null;
    public Player? CurrentPlayer { get; set; } 
    public IDice[] Dices { get; set; } 

    private readonly Map _map;
    private readonly List<Player> _players = new();
    private readonly Dictionary<Player, int> _playerRankDictionary = new();

    public IDictionary<Player, int> PlayerRankDictionary => _playerRankDictionary.AsReadOnly();
    
    public Game(string id, Map? map  = null, IDice[]? dices = null)
    {
        Id = id;
        _map = map ?? new Map(Array.Empty<Block[]>());
        Dices = dices ?? new IDice[2] { new Dice(), new Dice() };
    }

    public void AddPlayer(Player player) => _players.Add(player);

    public void AddPlayers(params Player[] players) => _players.AddRange(players);

    public void UpdatePlayerState(Player player)
    {
        player.UpdateState();

        if (player.IsBankrupt())
        {
            AddPlayerToRankList(player);
        }
    }

    public void Settlement()
    {
        // 資產 = 土地價格+升級價格+剩餘金額
        var playerList = from p in _players
                         where !p.IsBankrupt()
                         orderby p.Money + p.LandContracts.Sum(l => l.Value) ascending
                         select p;
        foreach (var player in playerList)
        {
            AddPlayerToRankList(player);
        }
    }

    public void SetPlayerToBlock(Player player, string blockId, Direction direction) => player.Chess.SetBlock(blockId, direction);

    // 玩家選擇方向
    // 1. 不能選擇回頭的方向
    // 2. 不能選擇沒有的方向
    public void PlayerSelectDirection(Player player, Direction direction)
    {
        player.SelectDirection(direction);
    }

    private Player FindPlayerById(string playerId)
    {
        var player = _players.Find(p => p.Id == playerId);
        if (player == null) throw new Exception($"找不到 {playerId} 玩家");
        return player;
    }

    public Block GetPlayerPosition(string playerId) => FindPlayerById(playerId).Chess.CurrentBlock;

    public Direction GetPlayerDirection(string playerId) => FindPlayerById(playerId).Chess.CurrentDirection;

    public void Initial()
    {
        Block startBlock = _map.FindBlockById("Start"); 
        foreach (Player player in _players)
        {
            Chess chess = new(player, _map, startBlock, Direction.Right);
            player.Chess = chess;
        }
        CurrentPlayer = _players[0];
    }

    /// <summary>
    /// 擲骰子 並且移動棋子直到遇到需要選擇方向的地方
    /// </summary>
    /// <param name="playerId"></param>
    /// <exception cref="Exception"></exception>
    public void PlayerRollDice(string playerId)
    {
        var player = FindPlayerById(playerId);
        if (player != CurrentPlayer) throw new Exception($"不是 {playerId} 的玩合");
        IDice[] dices = player.RollDice(Dices);
    }

    private void AddPlayerToRankList(Player player)
    {
        foreach (var rank in _playerRankDictionary)
        {
            _playerRankDictionary[rank.Key] += 1;
        }
        _playerRankDictionary.Add(player, 1);
    }

    public void SetPlayerToBlock(Player player, string v, object down)
    {
        throw new NotImplementedException();
    }
}

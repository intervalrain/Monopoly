using Application.Domain.Interfaces;

namespace Application.Domain;

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
                         orderby p.Money + p.LandContracts.Sum(l => (l.Land.House + 1) * l.Land.Price ) ascending
                         select p;
        foreach (var player in playerList)
        {
            AddPlayerToRankList(player);
        }
    }

    public void SetPlayerToBlock(Player player, string blockId, Direction direction) => player.Chess.SetBlock(blockId, direction);

    public Block GetPlayerPosition(string playerId) => GetPlayer(playerId).Chess.CurrentBlock;

    public Direction GetPlayerDirection(string playerId) => GetPlayer(playerId).Chess.CurrentDirection;

    // 玩家選擇方向
    // 1. 不能選擇回頭的方向
    // 2. 不能選擇沒有的方向
    public void PlayerSelectDirection(Player player, Direction direction)
    {
        player.SelectDirection(direction);
    }

    /// <summary>
    /// 把所有玩家放到起點，並面向右邊，輪到第一位玩家
    /// </summary>
    public void Initial()
    {
        Block startBlock = _map.FindBlockById("Start"); 
        foreach (Player player in _players)
        {
            player.Chess = new(player, _map, startBlock, Direction.Right);
        }
        CurrentPlayer = _players[0];
    }

    /// <summary>
    /// 擲骰子 並且移動棋子直到遇到需要選擇方向的地方
    /// </summary>
    /// <param name="playerId"></param>
    /// <exception cref="Exception"></exception>
    public IDice[] PlayerRollDice(string playerId)
    {
        Player player = GetPlayer(playerId);
        VerifyCurrentPlayer(player);
        IDice[] dices = player.RollDice(Dices);
        return dices;
    }

    public void EndAuction()
    {
        CurrentPlayer?.Auction.End();
    }

    public void PlayerSellLandContract(string playerId, string landId)
    {
        Player player = GetPlayer(playerId);
        VerifyCurrentPlayer(player);
        player.AuctionLandContract(landId);
    }

    public void PlayerBid(string playerId, int price)
    {
        var player = GetPlayer(playerId); 
        CurrentPlayer?.Auction.Bid(player, price);
    }

    public void MortgageLandContract(string playerId, string landId)
    {
        Player player = GetPlayer(playerId);
        VerifyCurrentPlayer(player);
        player.MortgageLandContract(landId);
    }

    public Player? GetOwner(Land location)
    {
        return location.GetOwner();
    }


    public bool CalculateToll(Land location, Player payer, Player payee, out decimal amount)
    {
        return location.CalculateToll(payer, payee, out amount);
    }


    public void PayToll(Player payer, Player payee, decimal amount)
    {
        payer.Money -= amount;
        payee.Money += amount;
    }

    public void BuyLand(Player player, string blockId)
    {
        if (player.Chess.CurrentBlock.Id != blockId)
            throw new Exception("必須在購買的土地上才可以購買");
        if (FindPlayerByLandId(blockId) != null)
            throw new Exception("非空地");

        Land land = (Land)_map.FindBlockById(blockId);
        if (land.Price > player.Money)
            throw new Exception("金額不足");

        player.Money -= land.Price;
        player.AddLandContracts(land);
    }

    public void UpgradeLand(Land land, int level = 1)
    {
        Player? player = FindPlayerByLandId(land.Id);
        if (player is null) throw new Exception($"{land.Id}不屬於任何人");
        land.Upgrade(player, level);
    }

    #region private function
    private Player GetPlayer(string playerId)
    {
        var player = _players.Find(p => p.Id == playerId);
        if (player is null)
        {
            throw new Exception($"找不到 {playerId} 玩家");
        }
        return player;
    }

    private void VerifyCurrentPlayer(Player player)
    {
        if (player != CurrentPlayer)
        {
            throw new Exception($"不是 {player.Id} 的玩合");
        }
    }

    private Player? FindPlayerByLandId(string blockId)
    {
        return _players.Where(p => p.LandContracts.Any(l => l.Land.Id == blockId)).FirstOrDefault();
    }

    private void AddPlayerToRankList(Player player)
    {
        foreach (var rank in _playerRankDictionary)
        {
            _playerRankDictionary[rank.Key] += 1;
        }
        _playerRankDictionary.Add(player, 1);
    }
    #endregion
}

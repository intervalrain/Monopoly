using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.Domain.Interfaces;

namespace Shared.Domain;

public class Player
{
    private Chess _chess;
	private readonly List<LandContract> _landContractList = new();
    private Auction _auction;

    public Player(string id, decimal money = Resource.DEFAULT_START_MONEY)
    {
        Id = id;
        Money = money;
        State = PlayerState.Normal;
    }

    public PlayerState State { get; private set; }
    public string Id { get; }
    public decimal Money { get; set; }

    public IList<LandContract> LandContracts => _landContractList.AsReadOnly();
	public Chess Chess { get => _chess; set => _chess = value; }
    public Auction Auction => _auction;

	public void UpdateState()
    {
        if (_landContractList.Count == 0 && Money == 0)
        {
            State = PlayerState.Bankrupt;
        }
    }

    public bool IsBankrupt() => State == PlayerState.Bankrupt;

    public void AddLandContract(LandContract landContract)
    {
        _landContractList.Add(landContract);
    }

    public void RemoveLandContract(LandContract landContract)
    {
        _landContractList.Remove(landContract);
    }

    public LandContract? FindLandContract(string blockId)
    {
        return _landContractList.Where(l => l.Land.Id == blockId).FirstOrDefault(); ;
    }

    public bool AddMoney(decimal money)
    {
        if (money < 0 && Money <= money)
        {
            return false;
        }
        Money += money;
        return true;
        
    }

    public void AuctionLandContract(string landId)
    {
        LandContract? landContract = FindLandContract(landId);
        if (landContract is null) throw new Exception($"找不到{landId}的地契");
        _auction = new Auction(landContract);
    }

    public IDice[] RollDice(IDice[] dices)
    {
        foreach (var dice in dices)
        {
            dice.Roll();
        }
        Chess.Move(dices.Sum(dice => dice.Value));
        return dices;
    }

    public void SelectDirection(Direction direction)
    {
        Chess.ChangeDirection(direction);
    }

    internal void Outcry(int money)
    {
        throw new NotImplementedException();
    }
}

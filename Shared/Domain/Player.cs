using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.Domain.Interfaces;

namespace Shared.Domain;

public class Player
{
	private List<LandContract> landContractList = new();

    public Player(string id, int money = Resource.DEFAULT_START_MONEY)
    {
        Id = id;
        Money = money;
    }

    public PlayerState State { get; private set; } = PlayerState.Normal;
    public string Id { get; }
    public int Money { get; set; }

    public IList<LandContract> LandContracts => landContractList.AsReadOnly();
	public Chess Chess { get; set; }

	public void UpdateState()
    {
        if (landContractList.Count == 0 && Money == 0)
        {
            State = PlayerState.Bankrupt;
        }
    }

    public bool IsBankrupt() => State == PlayerState.Bankrupt;

    public void AddLandContract(LandContract landContract)
    {
        landContractList.Add(landContract);
    }

    public void RemoveLandContract(LandContract landContract)
    {
        landContractList.Remove(landContract);
    }

    public bool FindLandContract(string blockId)
    {
        return landContractList.Any(l => l.Id == blockId);
    }

    public void AddMoney(int money)
    {
        Money += money;
    }

    public LandContract SellLandContract(string blockId)
    {
        return landContractList.First(l => l.Id == blockId);
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
}

using Domain.Common;
using Domain.Events;

namespace Domain.Models;

public class Land : Block
{
    private static readonly decimal[] RATE_OF_HOUSE = new decimal[] { 0.05m, 0.4m, 1, 3, 6, 10 };
    private static readonly decimal[] RATE_OF_LOT = new decimal[] { 0, 1, 1.3m, 2, 4, 8, 16 };
    private static readonly int MAX_HOUSE = 5;

    protected LandContract _landContract;
    protected readonly decimal _price;
    protected int _house = 0;
    protected string _lot;

    public decimal Price => _price;
    public int House => _house;
    public string Lot => _lot;

    public Land(string id, string lot, decimal price = 1000) : base(id)
    {
        _price = price;
        _lot = lot;
        _landContract = new LandContract(null, this);
    }

    public virtual void Upgrate()
    {
        _house++;
    }

    internal List<DomainEvent> PayToll(Player player)
    {
        Player? owner = _landContract.Owner;
        var events = new List<DomainEvent>();

        if (player.EndRoundFlag || owner.SuspendRounds > 0)
        {
            events.Add(new PlayerDoesntNeedToPayTollEvent(player.Id, player.Money));
        }
        else 
        {
            decimal amount = CalculateToll(owner);

            if (player.Money > amount)
            {
                player.EndRoundFlag = true;
                player.PayToll(owner, amount);
                events.Add(new PlayerPayTollEvent(player.Id, player.Money, owner.Id, owner.Money));
            }
            else
            {
                if (!player.LandContractList.Any(l => l.InMortgage))
                {
                    player.PayToll(owner, player.Money);
                    events.Add(new PlayerPayTollEvent(player.Id, player.Money, owner.Id, owner.Money));
                    events.Add(player.UpdateState());
                }
                else
                {
                    events.Add(new PlayerTooPoorToPayTollEvent(player.Id, player.Money, amount));
                }
            }
        }
        return events;
    }

    internal virtual decimal CalculateToll(Player owner)
    {
        int lotCount = owner.LandContractList.Count(t => t.Land.Lot == Lot);
        return _price * RATE_OF_HOUSE[_house] * RATE_OF_LOT[lotCount];
    }

    internal decimal GetPrice(LandUsage usage)
    {
        return usage switch
        {
            LandUsage.Mortage => _price * (1 + _house) * (decimal)0.7,
            LandUsage.Unsold => _price * (1 + _house) * (decimal)0.7,
            LandUsage.Redeem => _price * (1 + _house),
            LandUsage.Auction => _price * (1 + _house) * (decimal)0.5,
            _ => throw new Exception("Not valid land usage")
        };
    }

    internal override Player? GetOwner() => _landContract.Owner;

    internal override void UpdateOwner(Player? owner)
    {
        _landContract.Owner = owner;
    }

    internal virtual DomainEvent BuildHouse(Player player)
    {
        if (_house >= MAX_HOUSE)
        {
            return new HouseMaxEvent(player.Id, Id, _house);
        }
        else
        {
            player.Money -= _price;
            _house++;
            return new PlayerBuildHouseEvent(player.Id, Id, player.Money, _house);
        }
    }

    internal override DomainEvent OnBlockEvent(Player player)
    {
        Player? owner = GetOwner();
        var land = this;
        if (owner is null)
        {
            return new PlayerCanBuyLandEvent(player.Id, Id, land.Price);
        }
        else if (owner == player)
        {
            if (player.LandContractList.Any(l => l.Land.Id == Id && !l.InMortgage))
            {
                return new PlayerCanBuildHouseEvent(player.Id, land.Id, land.House, land.Price);
            }
        }
        else if (owner!.SuspendRounds <= 0)
        {
            return new PlayerNeedsToPayTollEvent(player.Id, owner.Id, land.CalculateToll(owner));
        }
        return DomainEvent.EmptyEvent;
    }

    internal override void DoBlockAction(Player player)
    {
        Player? owner = GetOwner();
        if (owner is null)
        {
            return;
        }
        if (owner.SuspendRounds <= 0)
        {
            player.EndRoundFlag = false;
        }
    }
}


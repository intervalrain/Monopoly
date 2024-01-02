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

    public virtual void Upgrate()
    {
        _house++;
    }

    internal override Player? GetOwner() => _landContract.Owner;

    internal override void UpdateOwner(Player? owner)
    {
        _landContract.Owner = owner;
    }
}


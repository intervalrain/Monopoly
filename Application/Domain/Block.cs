namespace Application.Domain;

public abstract class Block
{
	public string Id { get; }
	public Block? Up { get; set; }
	public Block? Down { get; set; }
	public Block? Left { get; set; }
	public Block? Right { get; set; }
	
	public List<Direction> Directions => new List<Direction>
	{
		Up is not null ? Direction.Up : Direction.None,
		Down is not null ? Direction.Down : Direction.None,
		Left is not null ? Direction.Left : Direction.None,
		Right is not null ? Direction.Right : Direction.None
	}.Where(d => d is not Direction.None).ToList();

	public Block(string id)
	{
		Id = id;
	}

	public Block? GetDirectionBlock(Direction d)
	{
		return d switch
		{
			Direction.Up => Up,
			Direction.Down => Down,
			Direction.Left => Left,
			Direction.Right => Right,
			_ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
		};
	}

	public virtual Player? GetOwner()
	{
		return null;
	}

	public virtual void UpdateOwner(Player owner)
	{
		throw new Exception("此地不可購買!");
	}

    public override string ToString()
    {
		return this.Id;
    }
}

public class Land : Block
{
	private readonly decimal _price;
	private int _house;
	private LandContract _landContract;
	private string _lot;
	
	public decimal Price => _price;
	public int House => _house;
	public string Lot => _lot;
	public LandContract LandContract => _landContract;

	public Land(string id, string lot = " ", decimal price = Resource.DEFAULT_LAND_PRICE)
		: base(id)
	{
		_lot = lot;
		_price = price;
		_landContract = new LandContract(null, this);
	}

	public void Upgrade(Player player, int level = 1)
	{
		while (level-- > 0 && _house < 5 && player.Money > _price)
		{
            _house++;
			player.Money -= _price;
        }
	}

    public bool CalculateToll(Player payer, Player payee, out decimal amount)
    {
		if (payee.Chess.CurrentBlock is Jail || payee.Chess.CurrentBlock is ParkingLot)
		{
			amount = 0;
			return false;
		}
		int lotCount = payee.LandContracts.Count(l => l.Land.Lot == _lot);
		amount = _price * Resource.RATE_OF_HOUSE[_house] * Resource.RATE_OF_LOT[lotCount];
		return payer.Money > amount;
    }

	public override Player? GetOwner()
    {
		return _landContract.Owner;
    }

    public override void UpdateOwner(Player? owner)
    {
		_landContract.Owner = owner;
    }
}

public class Start : Block
{
	public Start(string id)
		: base(id)
	{
	}
}

public class Station : Block
{
    private readonly decimal _price;
    private int _house;
    private string _area;
    public decimal Price => _price;
    public int House => _house;
	public string Area => _area;

    public Station(string id, decimal price = Resource.DEFAULT_STATION_PRICE)
        : base(id)
    {
		_area = "Station";
        _price = price;
    }
}

public class ParkingLot : Block
{
	public ParkingLot(string id)
		: base(id)
	{
	}
}

public class Jail : Block
{
	public Jail(string id)
		: base(id)
	{
	}
}
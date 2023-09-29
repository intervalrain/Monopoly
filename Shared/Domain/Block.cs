namespace Shared.Domain;

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
}

public class Land : Block
{
	private readonly decimal _price;
	private int _house;
	public decimal Price => _price;
	public int House => _house;
	public Land(string id, decimal price = 1000)
		: base(id)
	{
		_price = price;
	}

	public void Upgrade()
	{
		_house++;
	}
}

public class Start : Block
{
	public Start(string id)
		: base(id)
	{
	}
}
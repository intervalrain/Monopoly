using Shared.Domain.Enums;
using Shared.Interfaces;

namespace Shared.Domain;

public class Player
{
	private string _id;
	private int _money;
	private PlayerState _state;
    private Direction _direction;
    private IBlock? _position;

    public string Id => _id;
	public int Money => _money;
	public PlayerState State => _state;
	public Direction Direction => _direction; 
	public IBlock? Position => _position;
	public int DiceNum { get; set; }
	
    public Player(string id, int init = 5000)
	{
		_id = id;
		_state = PlayerState.Normal;
		_money = init;
		_position = null;
	}

	public void SetPosition(IBlock positon, Direction direction)
	{
		_position = positon;
		_direction = direction;
	}

	public bool AddMoney(int money)
	{
		if (_money + money <= 0)
		{
			_money = 0;
			_state = PlayerState.Bankrupt;
			return true;
		}
		_money += money;
		return false;
	}

	public void Pay(Player b, int money)
	{
		if (_money <= money)
		{
			b.AddMoney(_money);
			_money = 0;
			_state = PlayerState.Bankrupt;
		}
		else
		{
			b.AddMoney(money);
			_money -= money;
		}
	}

	public Player Has(int money)
	{
		_money = money;
		return this;
	}

	public Player Face(Direction direction)
	{
		_direction = direction;
		return this;
	}

	public Player At(IBlock block)
	{
		_position = block;
		return this;
	}

	public int RollDice(IDice?[] dices)
	{
		foreach (var dice in dices)
		{
			dice.Roll();
		}
		return dices.Sum(d => d.Value);
	}

	public void Move(int moves)
	{
		while (moves > 0)
		{
            if (moves > 0 && Position.Id == "Start") AddMoney(3000);
            Move();
			moves--;
		}
	}

	private void Move()
	{
		var currBlock = Position;
		var currDirection = Direction;
		var nextBlock = currBlock.Next(currDirection);

		if (nextBlock == currBlock)
		{
			return;
		}
		else
		{
			var direction = currBlock.GetRelation(nextBlock);
			_direction = direction;
			_position = nextBlock;
		}
	}

	public void BuyLand(IBlock block)
	{
		var contract = block.Contract;
		if (contract.Owner != null) return;
		if (Money > contract.Price)
		{
			AddMoney(-contract.Price);
			contract.SetOwner(this);
		}
	}

    public void SellLand(IBlock block)
    {
        var contract = block.Contract;
        if (contract.Owner != this) return;
        AddMoney(contract.Value);
        contract.SetOwner(null);
    }

    public override string ToString()
    {
		return Id;
    }
}

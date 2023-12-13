namespace Domain;

public class Contract
{
	private Player? _owner = null;
	private int _price = 1000;
	private int _level = 0;

	public int Price => _price;
	public int Value => _price * (1 + _level);
	public int Toll
	{
		get
		{
			switch(_level)
			{
				case 0: return (int)(0.05 * Price);
				case 1: return (int)(0.4 * Price);
				case 2: return (int)(1 * Price);
				case 3: return (int)(3 * Price);
				case 4: return (int)(6 * Price);
				case 5: return (int)(10 * Price);
				default: throw new Exception("Not avaible price");
			}
		}
	}
	public Player? Owner => _owner;
	public int Level => _level;

	public void Upgrade()
	{
		_level++;
	}

	public void Upgrade(int level)
	{
		while (level-- > 0 && _level < 5)
		{
			Upgrade();
		}
	}

    public void SetOwner(Player? player)
    {
		_owner = player;
    }
}


namespace Shared.Domain;

public class Contract
{
	private Player? _owner = null;
	private int _price = 1000;
	private int _level = 0;

	public int Price => _price;
	public int Value => _price * (1 + _level);
	public Player? Owner => _owner;
	public int Level => _level;

	public void Upgrade()
	{
		if (_owner == null) return;
		if (_owner.Money > _price)
		{
			_owner.AddMoney(-_price);
			_level++;
		}
	}

    public void SetOwner(Player player)
    {
		_owner = player;
    }
}


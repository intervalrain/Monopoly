using System;
namespace Shared.Domain;

public class Player
{
	private string _id;
	private int _money;
	private PlayerState _state;

	public string Id => _id;
	public int Money => _money;
	public PlayerState State => _state;

	public Player(string id, int init = 5000)
	{
		_id = id;
		_state = PlayerState.Normal;
		_money = init;
	}

	public bool AddMoney(int money)
	{
		if (_money + money <= 0)
		{
			money = 0;
			_state = PlayerState.Bankrupt;
			return true;
		}
		money += money;
		return false;
	}
}

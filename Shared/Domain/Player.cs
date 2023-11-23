using System;
namespace Shared.Domain;

public class Player
{
	private string _id;
	public string Id => _id;
	public PlayerState State { get; set; }

	public Player(string id)
	{
		_id = id;
		State = PlayerState.Normal;
	}
}

using Domain.Interfaces;

namespace Domain;

public class Dice : IDice
{
	public int Value { get; private set; }

	public void Roll()
	{
		Random random = new();
		Value = random.Next(1, 6);
	}
}

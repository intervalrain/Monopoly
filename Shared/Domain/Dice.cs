namespace Shared.Domain;

public class Dice
{
	private int _diceNum;
	private int _steps;

	public int DiceNum => _diceNum;
	public int steps => _steps;

	public Dice(int num)
	{
		_diceNum = num;
	}

	public void Roll()
	{
		_steps = 0;
		for (int i = 0; i < _diceNum; i++)
		{
            _steps += new Random().Next(1, 7);
        }
	}
}

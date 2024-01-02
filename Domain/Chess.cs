namespace Domain;

public class Chess
{
	private readonly Player _player;
	private string _currentBlockId;
	private Direction _currentDirection;
	private int _remainingSteps;

	public Direction CurrentDirection => _currentDirection;
	public int RemainingSteps => _remainingSteps;
	public string CurrentBlockId => _currentBlockId;

	public Chess(Player player, string currentBlockId, Direction currentDirection, int remainingSteps)
	{
		_player = player;
		_currentBlockId = currentBlockId;
		_currentDirection = currentDirection;
		_remainingSteps = remainingSteps;
	}

	private List<Direction> DirectionOpetions(Map map)
	{
		var directions = map.FindBlockById(CurrentBlockId).Directions;
		directions.Remove(CurrentDirection.Opposite());
		return directions;
	}
}

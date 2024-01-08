using Domain.Common;
using Domain.Events;

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

	private IEnumerable<DomainEvent> Move(Map map)
	{
		while (_remainingSteps > 0)
		{
			var nextBlock = map.FindBlockById(CurrentBlockId).GetDirectionBlock(CurrentDirection) ?? throw new Exception("找不到下一個區塊");
			_currentBlockId = nextBlock.Id;
			_remainingSteps--;
			if (CurrentBlockId == "Start" && RemainingSteps > 0)
			{
				_player.Money += 3000;
				yield return new ThroughStartEvent(_player.Id, 3000, _player.Money);
			}
			var directions = DirectionOptions(map);
			if (directions.Count > 1)
			{
				yield return new PlayerNeedToChooseDirectionEvent(
					_player.Id,
					directions.ToArray());
				yield break;
			}
			_currentDirection = directions.FirstOrDefault();
			yield return new ChessMovedEvent(_player.Id, CurrentBlockId, CurrentDirection, RemainingSteps);
		}
		map.FindBlockById(CurrentBlockId).DoBlockAction(_player);
		yield return map.FindBlockById(CurrentBlockId).OnBlockEvent(_player);
	}

	public IEnumerable<DomainEvent> Move(Map map, int steps)
	{
		_remainingSteps = steps;
		return Move(map);
	}

	internal IEnumerable<DomainEvent> ChangeDirection(Map map, Direction direction)
	{
		var events = new List<DomainEvent>();
		_currentDirection = direction;
		yield return new PlayerChooseDirectionEvent(_player.Id, CurrentDirection);

		foreach (var e in Move(map))
		{
			yield return e;
		}
	}

	private List<Direction> DirectionOptions(Map map)
	{
		var directions = map.FindBlockById(CurrentBlockId).Directions;
		directions.Remove(CurrentDirection.Opposite());
		return directions;
	}
}

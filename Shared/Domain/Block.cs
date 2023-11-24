using Shared.Interfaces;
using Shared.Domain.Enums;
using Shared.Common.Extensions;

namespace Shared.Domain;

public class Block : IBlock
{
	private string _id;
	private Facility _facility;
	private Dictionary<Direction, IBlock> _neigherBlocks = new();
	private bool _stop;

	public string Id => _id;
	public Facility Facility => _facility;

	public IBlock? Up => _neigherBlocks.ContainsKey(Direction.Up) ? _neigherBlocks[Direction.Up] : null;
    public IBlock? Down => _neigherBlocks.ContainsKey(Direction.Down) ? _neigherBlocks[Direction.Down] : null;
    public IBlock? Left => _neigherBlocks.ContainsKey(Direction.Left) ? _neigherBlocks[Direction.Left] : null;
    public IBlock? Right => _neigherBlocks.ContainsKey(Direction.Right) ? _neigherBlocks[Direction.Right] : null;
	public IBlock[] Neighbors => _neigherBlocks.Select(n => n.Value).ToArray();

    public Block(string id, Facility facility)
	{
		_stop = false;
		_id = id;
		_facility = facility;
	}

	public void SetConnection(Direction dir, IBlock block)
	{
		_neigherBlocks.Add(dir, block);
	}

	public virtual IBlock Next(Direction dir)
	{
		if (_stop)
		{
			_stop = false;
			return this;
		}
		Direction oppo = dir.Opposite();
		var nexts = _neigherBlocks.Where(n => n.Key != oppo).Select(n => n.Key).ToList();
		int rand = new Random().Next(nexts.Count);
		var next = nexts[rand];
		return _neigherBlocks.First(n => n.Key == next).Value;
    }

	public virtual Direction GetRelation(IBlock block)
	{
		if (!_neigherBlocks.ContainsValue(block))
			throw new ArgumentOutOfRangeException(block.Id);
		return _neigherBlocks.First(n => n.Value == block).Key;
	}

    public override string ToString()
    {
		return Id;
    }
}

using System;
namespace Shared.Domain
{
	public abstract class Block
	{
		public string Id { get; }
		public Block? Up { get; set; }
		public Block? Down { get; set; }
		public Block? Left { get; set; }
		public Block? Right { get; set; }
		public List<Direction> Directions => new List<Direction>
		{
			Up is not null ? Direction.Up : Direction.None,
			Down is not null ? Direction.Down : Direction.None,
			Left is not null ? Direction.Left : Direction.None,
			Right is not null ? Direction.Right : Direction.None
		}.Where(d => d is not Direction.None).ToList();

		public Block(string id)
		{
			Id = id;
		}

		public Block? GetDirectionBlock(Direction d)
		{
			return d switch
			{
				Direction.Up => Up,
				Direction.Down => Down,
				Direction.Left => Left,
				Direction.Right => Right,
				_ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
			};
		}
	}

	public class Land : Block
	{
		public Land(string id)
			: base(id)
		{
		}
	}
}


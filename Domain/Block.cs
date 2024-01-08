using Domain.Common;

namespace Domain;

public abstract class Block
{
    public string Id { get; }
    internal Block? Up { get; set; }
    internal Block? Down { get; set; }
    internal Block? Left { get; set; }
    internal Block? Right { get; set; }

    internal List<Direction> Directions => new List<Direction>()
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

    internal Block? GetDirectionBlock(Direction d)
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

    internal virtual Player? GetOwner()
    {
        return null;
    }

    internal virtual void UpdateOwner(Player? owner)
    {
        throw new Exception("此地不可購買!");
    }

    internal abstract DomainEvent OnBlockEvent(Player player);
    internal abstract void DoBlockAction(Player player);
}
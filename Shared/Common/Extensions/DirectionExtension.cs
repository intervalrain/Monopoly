using Shared.Domain.Enums;

namespace Shared.Common.Extensions;

public static class DirectionExtension
{
    public static Direction Opposite(this Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
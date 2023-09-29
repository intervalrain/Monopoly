using System;

namespace Shared.Domain;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}

public static class DirectionExtension
{
    public static Direction Opposite(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
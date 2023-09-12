using System;
namespace Shared.Domain
{
	public class Direction
	{
        public enum Enumerates
        {
            Up,
            Down,
            Left,
            Right
        }

        public static int[][] Array = new int[][]
        {
            new int[] { -1, 0 },
            new int[] { 1, 0 },
            new int[] { 0, -1 },
            new int[] { 0, 1 }
        };

        public static Direction.Enumerates GetRandom()
        {
            Random random = new Random();
            int rand = random.Next() % 4;
            if (rand == 0) return Enumerates.Up;
            else if (rand == 1) return Enumerates.Down;
            else if (rand == 2) return Enumerates.Left;
            return Enumerates.Right;
        }
    }
}


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
    }
}


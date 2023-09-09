using System;
namespace Shared.Domain
{
	public class Map
	{
		private readonly Dictionary<string, IBlock> BlockMap = new(); 
		private readonly Dictionary<Player, IBlock> playerPosition = new();

		public Map(IBlock[][] blocks)
		{
            int[][] dirc = Direction.Array;
            
            for (int i = 0; i < blocks.Length; i++)
            {
                for (int j = 0; j < blocks[0].Length; j++)
                {
                    IBlock block = blocks[i][j]; 
                    if (block != null)
                    {
                        BlockMap[block.Id] = block;

                        int ix = i + dirc[0][0];
                        int jx = j + dirc[0][1];
                        if (ix >= 0 && ix < blocks.Length && jx >= 0 && jx < blocks[0].Length && blocks[ix][jx] != null)
                        {
                            block.Up = blocks[ix][jx];
                        }

                        ix = i + dirc[1][0];
                        jx = j + dirc[1][1];
                        if (ix >= 0 && ix < blocks.Length && jx >= 0 && jx < blocks[0].Length && blocks[ix][jx] != null)
                        {
                            block.Down = blocks[ix][jx];
                        }

                        ix = i + dirc[2][0];
                        jx = j + dirc[2][1];
                        if (ix >= 0 && ix < blocks.Length && jx >= 0 && jx < blocks[0].Length && blocks[ix][jx] != null)
                        {
                            block.Left = blocks[ix][jx];
                        }

                        ix = i + dirc[3][0];
                        jx = j + dirc[3][1];
                        if (ix >= 0 && ix < blocks.Length && jx >= 0 && jx < blocks[0].Length && blocks[ix][jx] != null)
                        {
                            block.Right = blocks[ix][jx];
                        }
                    }
                }
            }
		}

        public void SetPosition(Player playerA, string blockId)
        {
            BlockMap.TryGetValue(blockId, out IBlock? block);
            if (block != null)
            {
                SetPosition(playerA, block);
            }    
        }
        public void SetPosition(Player playerA, IBlock block)
        {
            playerPosition[playerA] = block;
        }

        public void Move(Player player, int point)
        {
            if (point == 0) return;
            Direction.Enumerates direction = player.Direction;
            IBlock? currBlock = GetPosition(player);
            IEnumerable<IBlock?> nextBlocks;
            
            switch (direction)
            {
                case Direction.Enumerates.Up:
                    nextBlocks = new[] { currBlock.Up, currBlock.Left, currBlock.Right }.Where(x => x != null);
                    break;
                case Direction.Enumerates.Down:
                    nextBlocks = new[] { currBlock.Down, currBlock.Right, currBlock.Left }.Where(x => x != null);
                    break;
                case Direction.Enumerates.Left:
                    nextBlocks = new[] { currBlock.Left, currBlock.Down, currBlock.Up }.Where(x => x != null);
                    break;
                case Direction.Enumerates.Right:
                    nextBlocks = new[] { currBlock.Right, currBlock.Up, currBlock.Down }.Where(x => x != null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            Random random = new Random();
            int rand = random.Next() % nextBlocks.Count();
            IBlock? nextBlock = nextBlocks.ToArray()[rand];
            Direction.Enumerates nextDirection = GetDirection(currBlock, nextBlock);
            if (nextBlock != null)
            {
                SetPosition(player, nextBlock);
            }
            player.Direction = nextDirection;

            Move(player, point - 1);
        }

        public IBlock GetPosition(Player player)
        {
            return playerPosition[player];
        }

        public Direction.Enumerates GetDirection(IBlock? curr, IBlock? next)
        {
            if (next == curr.Up) return Direction.Enumerates.Up;
            else if (next == curr.Down) return Direction.Enumerates.Down;
            else if (next == curr.Left) return Direction.Enumerates.Left;
            else if (next == curr.Right) return Direction.Enumerates.Right;
            throw new ArgumentOutOfRangeException(curr.Id);
        }
    }
}


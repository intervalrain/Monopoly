using System;
namespace Shared.Domain
{
	public class Map
	{
		private readonly Dictionary<string, Block> _blockMap = new(); 
		private readonly Dictionary<Player, Block> _playerPosition = new();

		public Map(Block?[][] blocks)
		{
            int[][] dirc = new int[][]
            {
                new int[]{ -1, 0 },
                new int[]{ 1, 0 },
                new int[]{ 0, -1 },
                new int[]{ 0, 1 }
            };
            
            for (int i = 0; i < blocks.Length; i++)
            {
                for (int j = 0; j < blocks[0].Length; j++)
                {
                    Block? block = blocks[i][j]; 
                    if (block != null)
                    {
                        _blockMap[block.Id] = block;

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

        public Block? GetBlockById(string blockId)
        {
            _blockMap.TryGetValue(blockId, out Block? block);
            return block;
        }

        public void SetPosition(Player player, string blockId, Direction direction)
        {
            Block? block = GetBlockById(blockId);
            if (block == null) return;
            SetPosition(player, block);
            player.Direction = direction;
        }

        private void SetPosition(Player player, Block? block)
        {
            if (block == null) return;
            _playerPosition[player] = block;
        }

        public void MovePlayer(Player player, int point)
        {
            if (point == 0) return;
            Direction direction = player.Direction;
            Block? currBlock = GetPlayerPositionedBlock(player);
            IEnumerable<Block?> nextBlocks;
            
            switch (direction)
            {
                case Direction.Up:
                    nextBlocks = new[] { currBlock.Up, currBlock.Left, currBlock.Right }.Where(x => x != null);
                    break;
                case Direction.Down:
                    nextBlocks = new[] { currBlock.Down, currBlock.Right, currBlock.Left }.Where(x => x != null);
                    break;
                case Direction.Left:
                    nextBlocks = new[] { currBlock.Left, currBlock.Down, currBlock.Up }.Where(x => x != null);
                    break;
                case Direction.Right:
                    nextBlocks = new[] { currBlock.Right, currBlock.Up, currBlock.Down }.Where(x => x != null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            Random random = new Random();
            int rand = random.Next() % nextBlocks.Count();
            Block? nextBlock = nextBlocks.ToArray()[rand];
            Direction nextDirection = GetDirection(currBlock, nextBlock);
            if (nextBlock != null)
            {
                SetPosition(player, nextBlock);
            }
            player.Direction = nextDirection;

            MovePlayer(player, point - 1);
        }

        private Block GetPlayerPositionedBlock(Player player)
        {
            return _playerPosition[player];
        }

        public string GetPlayerPositionedBlockId(Player player)
        {
            return GetPlayerPositionedBlock(player).Id;
        }

        public Direction GetDirection(Block? curr, Block? next)
        {
            if (next == curr.Up) return Direction.Up;
            else if (next == curr.Down) return Direction.Down;
            else if (next == curr.Left) return Direction.Left;
            else if (next == curr.Right) return Direction.Right;
            throw new ArgumentOutOfRangeException(curr.Id);
        }

        public static Direction Opposite(Direction direction)
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
}


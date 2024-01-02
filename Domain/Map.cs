namespace Domain;

public class Map
{
    private readonly Block?[][] _blocks;
    private Dictionary<string, Block?> _blockMap = new();

    public Map(string id, Block?[][] blocks)
    {
        Id = id;
        _blocks = blocks;
        GererateBlockConnection(blocks, _blockMap);
    }
    public string Id { get; init; }
    public Block?[][] Blocks => _blocks;

    private static void GererateBlockConnection(Block?[][] blocks, Dictionary<string, Block?> blockMap)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            for (int j = 0; j < blocks[i].Length; j++)
            {
                var block = blocks[i][j];
                if (block is null) continue;
                blockMap.Add(block.Id, block);

                if (i > 0)
                {
                    block.Up = blocks[i - 1][j];
                }
                if (i < blocks.Length - 1)
                {
                    block.Down = blocks[i + 1][j];
                }
                if (j > 0)
                {
                    block.Left = blocks[i][j - 1];
                }
                if (j < blocks[i].Length - 1)
                {
                    block.Right = blocks[i][j + 1];
                }
            }
        }
    }

    public Block FindBlockById(string blockId)
    {
        if (!_blockMap.ContainsKey(blockId))
        {
            throw new Exception("找不到該區塊");
        }
        return _blockMap[blockId]!;
    }

    public T FindBlockById<T>(string blockId) where T : Block
    {
        return (T)FindBlockById(blockId);
    }
}

public static class DirectionExtension
{
    internal static Direction Opposite(this Direction direction)
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

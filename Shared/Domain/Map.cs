using System;
namespace Shared.Domain;

public class Map
{
    private readonly Dictionary<string, Block> _blockMap = new();

    public Map(Block?[][] blocks)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            for (int j = 0; j < blocks[0].Length; j++)
            {
                var block = blocks[i][j];
                if (block is null) continue;
                _blockMap.Add(block.Id, block);

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
        _blockMap.TryGetValue(blockId, out Block? block);
        if (block == null) throw new Exception($"找不到 {blockId} 區塊");
        return block;
    }
}

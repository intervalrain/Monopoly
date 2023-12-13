using Shared.Domain.Maps;
using Shared.Domain.Enums;
using Shared.Interfaces;
namespace Shared.Domain;

public class Map
{
	private readonly IBlock?[][] _blocks;
	private readonly Dictionary<string, IBlock> _blockMapping = new();

	public IEnumerable<IBlock> Blocks => _blockMapping.Select(b => b.Value); 

	public Map(IBlock?[][] blocks)
	{
		_blocks = blocks;
		MakeConnection();
	}

	public IBlock FindBlockById(string id)
	{
		if (!_blockMapping.ContainsKey(id))
			throw new KeyNotFoundException(id);
		return _blockMapping[id];
	}

	public int GetLandCount(Player p)
	{
		return Blocks.Where(b => b.Contract.Owner == p).Count();
	}

	private void MakeConnection()
	{
		object[][] dirc = new object[4][]
		{
			new object[] { 1,0, Direction.Down },
			new object[] { 0,1, Direction.Right },
			new object[] { -1,0, Direction.Up },
			new object[] { 0,-1, Direction.Left }
		};
		for (int i = 0; i < _blocks.Length; i++)
		{
			for (int j = 0; j < _blocks[i].Length; j++)
			{
				var block = _blocks[i][j];
				if (block == null) continue;
                _blockMapping.Add(block.Id, block);
                foreach (var d in dirc)
				{
					int i2 = i + Convert.ToInt32(d[0]);
					int j2 = j + Convert.ToInt32(d[1]);
					Direction dir = (Direction)d[2]; 
					if (i2 < 0 || i2 >= _blocks.Length || j2 < 0 || j2 >= _blocks.Length || _blocks[i2][j2] == null) continue;
					block.SetConnection(dir, _blocks[i2][j2]!);
				}
				
			}
		}
	}
}

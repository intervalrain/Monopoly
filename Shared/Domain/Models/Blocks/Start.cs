using Shared.Domain.Enums;

namespace Shared.Domain.Models.Blocks;

public class Start : Block
{
	public Start(string id) : base(id, Facility.Start)
	{
	}
}

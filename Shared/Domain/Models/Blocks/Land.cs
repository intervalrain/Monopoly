using Shared.Domain.Enums;

namespace Shared.Domain.Models.Blocks;

public class Land : Block
{
	public Land(string id) : base(id, Facility.Land)
	{
	}
}

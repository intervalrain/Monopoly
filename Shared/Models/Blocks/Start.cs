using Domain.Enums;

namespace Domain.Models.Blocks;

public class Start : Block
{
	public Start(string id) : base(id, Facility.Start)
	{
	}
}

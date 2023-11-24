using Shared.Domain.Enums;

namespace Shared.Domain.Models.Blocks;

public class Prison : Block 
{
    public Prison(string id) : base(id, Facility.Prison)
    {
    }
}

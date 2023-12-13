using Domain.Enums;

namespace Domain.Models.Blocks;

public class Prison : Block 
{
    public Prison(string id) : base(id, Facility.Prison)
    {
    }
}

using Shared.Domain.Enums;

namespace Shared.Domain.Models.Blocks;

public class Station : Block
{
    public Station(string id) : base(id, Facility.Station)
    {
    }
}

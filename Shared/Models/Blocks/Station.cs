using Domain.Enums;

namespace Domain.Models.Blocks;

public class Station : Block
{
    public Station(string id) : base(id, Facility.Station)
    {
    }
}

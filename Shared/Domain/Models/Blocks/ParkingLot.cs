using Shared.Domain.Enums;

namespace Shared.Domain.Models.Blocks;

public class ParkingLot : Block
{
    public ParkingLot(string id) : base(id, Facility.ParkingLot)
    {
    }
}

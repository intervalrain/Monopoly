using Domain.Enums;

namespace Domain.Models.Blocks;

public class ParkingLot : Block
{
    public ParkingLot(string id) : base(id, Facility.ParkingLot)
    {
    }
}

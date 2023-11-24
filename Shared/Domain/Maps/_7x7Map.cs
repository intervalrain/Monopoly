using Shared.Domain.Models.Blocks;
using Shared.Interfaces;

namespace Shared.Domain.Maps;

public class _7x7Map
{
	// | ST | A1 | s1 | A2 | A3 |    |    |
	// | F4 |    |    |    | A4 |    |    |
	// | s4 |    | B5 | B6 | PL | C1 | C2 |
	// | F3 |    | B4 |    | B1 |    | C3
	// | F2 | F1 | PS | B3 | B2 |    | s2 |
	// |    |    | E3 |    |    |    | D1 |
	// |    |    | E2 | E1 | s3 | D3 | D2 |
	public static IBlock?[][] Standard7x7 =>
		new IBlock?[][]
		{
			new IBlock?[] { new Start("Start"),      new Land("A1"), new Station("Station1"), new Land("A2"), new Land("A3"),               null,           null },
			new IBlock?[] { new Land("F4"),          null,           null,                    null,           new Land("A4"),               null,           null },
			new IBlock?[] { new Station("Station4"), null,           new Land("B5"),          new Land("B6"), new ParkingLot("ParkingLot"), new Land("C1"), new Land("C2") },
            new IBlock?[] { new Land("F3"),          null,           new Land("B4"),          null,           new Land("B1"),               null,           new Land("C3") },
            new IBlock?[] { new Land("F2"),          new Land("F1"), new Prison("Prison"),    new Land("B3"), new Land("B2"),               null,           new Station("Station2") },
            new IBlock?[] { null,                    null,           new Land("E3"),          null,           null,                         null,           new Land("D1") },
            new IBlock?[] { null,                    null,           new Land("E2"),          new Land("E1"), new Station("Station3"),      new Land("D3"), new Land("D2") }
        };
}

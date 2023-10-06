namespace Application.Domain.Maps;

public class SevenXSevenMap : Map
{
    public SevenXSevenMap() : base(Blocks)
    {
    }

    /**
    * s o z o o         s: start       *1
    * o       o         z: station     *4
    * z   o o p o o     p: parking lot *1
    * o   o   o   o     x: prison      *1
    * o o x o o   z
    *     o       o
    *     o o z o o
    */
    public static Block?[][] Blocks => new Block?[][]
    {
        new Block?[] { new Start("Start"),      new Land("A1", "A"), new Station("Station1"), new Land("A2", "A"), new Land("A3", "A"),          null,                null },
        new Block?[] { new Land("F4", "F"),     null,                null,                    null,                new Land("A4", "A"),          null,                null },
        new Block?[] { new Station("Station4"), null,                new Land("B5", "B"),     new Land("B6", "B"), new ParkingLot("ParkingLot"), new Land("C1", "C"), new Land("C2", "C") },
        new Block?[] { new Land("F3", "F"),     null,                new Land("B4", "B"),     null,                new Land("B1", "B"),          null,                new Land("C3", "C") },
        new Block?[] { new Land("F2", "F"),     new Land("F1", "F"), new Jail("Jail"),        new Land("B3", "B"), new Land("B2", "B"),          null,                new Station("Station2") },
        new Block?[] { null,                    null,                new Land("E3", "E"),     null,                null,                         null,                new Land("D1", "D") },
        new Block?[] { null,                    null,                new Land("E2", "E"),     new Land("E1", "E"), new Station("Station3"),      new Land("D3", "D"), new Land("D2", "D") }
    };

}
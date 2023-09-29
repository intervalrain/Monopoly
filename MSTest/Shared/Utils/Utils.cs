using Moq;
using Shared.Domain;
using Shared.Domain.Interfaces;

namespace Test.Shared;

public class Utils
{
    /**
        * s o z o o         s: start       *1
        * o       o         z: station     *4
        * z   o o p o o     p: parking lot *1
        * o   o   o   o     x: prison      *1
        * o o x o o   z
        *     o       o
        *     o o z o o
        */
    public static Block?[][] SevenXSevenMap => new Block?[][]
    {
        new Block?[] { new Start("Start"),   new Land("A1"), new Land("Station1"), new Land("A2"), new Land("A3"),         null,           null },
        new Block?[] { new Land("F4"),       null,           null,                 null,           new Land("A4"),         null,           null },
        new Block?[] { new Land("Station4"), null,           new Land("B5"),       new Land("B6"), new Land("ParkingLot"), new Land("C1"), new Land("C2") },
        new Block?[] { new Land("F3"),       null,           new Land("B4"),       null,           new Land("B1"),         null,           new Land("C3") },
        new Block?[] { new Land("F2"),       new Land("F1"), new Land("Prison"),   new Land("B3"), new Land("B2"),         null,           new Land("Station2") },
        new Block?[] { null,                 null,           new Land("E3"),       null,           null,                   null,           new Land("D1") },
        new Block?[] { null,                 null,           new Land("E2"),       new Land("E1"), new Land("Station3"),   new Land("D3"), new Land("D2") }
    };

    public static IDice[]? MockDice(params int[] diceValues)
    {
        var dice = new IDice[diceValues.Length];
        for (int i = 0; i < diceValues.Length; i++)
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(x => x.Roll());
            mockDice.Setup(x => x.Value).Returns(diceValues[i]);
            dice[i] = mockDice.Object;
        }

        return dice;
    }
}

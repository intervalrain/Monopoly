using System;
using Shared.Domain;

namespace Test.Shared.Utils
{
	public class MapLibrary
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
        public static IBlock?[][] _7x7Map => new IBlock?[][]
        {
            new IBlock?[] { new Block("Start"),    new Block("A1"), new Block("Station1"), new Block("A2"), new Block("A3"),         null,            null },
            new IBlock?[] { new Block("F4"),       null,            null,                  null,            new Block("A4"),         null,            null },
            new IBlock?[] { new Block("Station4"), null,            new Block("B5"),       new Block("B6"), new Block("ParkingLot"), new Block("C1"), new Block("C2") },
            new IBlock?[] { new Block("F3"),       null,            new Block("B4"),       null,            new Block("B1"),         null,            new Block("C3") },
            new IBlock?[] { new Block("F2"),       new Block("F1"), new Block("Prison"),   new Block("B3"), new Block("B2"),         null,            new Block("Station2") },
            new IBlock?[] { null,                  null,            new Block("E3"),       null,            null,                    null,            new Block("D1") },
            new IBlock?[] { null,                  null,            new Block("E2"),       new Block("E1"), new Block("Station3"),   new Block("D3"), new Block("D2") }
        };
    }
}


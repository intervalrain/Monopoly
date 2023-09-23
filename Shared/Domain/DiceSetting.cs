using System;
namespace Shared.Domain
{
	public class DiceSetting
	{
		public DiceSetting(int numberOfDice = 1, int min = 1, int max = 6)
		{
			NumberOfDice = numberOfDice;
            Min = min;
            Max = max;
		}

        public int NumberOfDice { get; set; }
		public int Min { get; set; }
        public int Max { get; set; }

    }
}


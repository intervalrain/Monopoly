using System;
namespace Shared.Domain
{
	public class LandContract
	{
		private int price;
		private Player? owner;
		private int level;

		public LandContract(int price, Player? owner)
		{
			this.price = price;
			this.owner = owner;
			this.level = 0;
		}

		public int Price => price;

		public int Level => level;

		public void Upgrade()
		{
			this.level++;
		}
	}
}


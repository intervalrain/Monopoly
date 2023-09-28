using System;
namespace Shared.Domain
{
	public class LandContract
	{
		public int Price { get; set; }
        public int House { get; set; }
        public string Id { get; set; }

        private int _defaultPrice;
		private int _sellPrice = 0;
		private Player? _owner;
        private Player? _buyer = null;

		public LandContract(int price, Player? owner, string blockId)
		{
			Price = price;
			_defaultPrice = (int)(price * 0.5);
			_owner = owner;
			House = 0;
			Id = blockId;
		}

		public int Value => Price * (1 + House);


        public bool HasOutcry() => _sellPrice > 0;

        public void SetOutcry(Player player, int price)
        {
			if (price > _sellPrice && player.Money >= price)
			{
				_buyer = player;
				_sellPrice = price;
			}
        }

        public void Upgrade()
		{
			House++;
		}

		public void Sell()
		{
			int _price;
            Player? buyer = _buyer;
			Player? seller = _owner; 

			if (_buyer != null)
			{
				_price = _sellPrice;
				_sellPrice = 0;
				_owner = buyer;
				_buyer = null;
				buyer?.AddLandContract(this);
				buyer?.AddMoney(-_price);
			}
			else
			{
				_price = (int)(Price * 0.7);
				_sellPrice = 0;
				_owner = null;
				House = 0;
			}
			seller?.AddMoney(_price);
			seller?.RemoveLandContract(this);
        }
    }
}


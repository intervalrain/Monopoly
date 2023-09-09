using System;
namespace Shared.Domain
{
	public class LandContract
	{
		private int _price;
		private Player? _owner;
		private int _level;
		private string _id;
		private Player? _bidder;
		private int _bid;

		public LandContract(int price, string blockId)
		{
			this._price = price;
			this._level = 0;
			this._id = blockId;
		}

		public int Price => _price;

		public int Level => _level;

		public string Id => _id;

		public int Value => _price * (1 + _level);

		public void Upgrade()
		{
			this._level++;
		}

		public void SetOwner(Player? player)
		{
			this._owner = player;
		}

        public bool HasOutcry()
        {
			return _bidder != null;
        }

		public void StartBid()
		{
			_bid = (int)(Resource.DEFAULT_BID_START * Value);
		}

        public void SetOutcry(Player player, int price)
		{
            if (player.Money <= price || price <= _bid)
			{
				return;
			}
			_bidder = player;
			_bid = price;
        }

		public void Sell()
		{
			Player? buyer = _bidder;
			Player? seller = _owner;
            if (seller != null)
            {
                seller.AddMoney(_bid);
				seller.RemoveLandContract(this);
            }
            if (buyer != null)
			{
                buyer.AddMoney(-_bid);
				buyer.AddLandContract(this);
				_owner = buyer;
            }
			_bidder = null;	
        }
    }
}


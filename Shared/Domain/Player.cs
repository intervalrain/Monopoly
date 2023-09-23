using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;

namespace Shared.Domain
{
	public class Player
	{
		private List<LandContract> _landContracts = new();
		private int _money;

        public PlayerState State { get; private set; } = PlayerState.Normal;
        public string Id { get; }
        public int Money => _money;
		public Direction Direction { get; set; }

        public Player(string id, int money = Resource.DEFAULT_START_MONEY)
		{
			Id = id;
			_money = money;
		}

		public void SetState(PlayerState playerState)
		{
			State = playerState;
		}

		public void AddLandContract(LandContract landContract)
		{
			landContract.SetOwner(this);
			this._landContracts.Add(landContract);
		}

        public void RemoveLandContract(LandContract landContract)
        {
			landContract.SetOwner(null);
            this._landContracts.Remove(landContract);
        }

        public void AddMoney(int money)
		{
			this._money += money;
		}

        public LandContract SellLandContract(string blockId)
        {
			LandContract? landContract = _landContracts.FirstOrDefault(l => l.Id == blockId);
			if (landContract == null)
			{
				throw new NullReferenceException(blockId + " does not belong to player " + Id);
			}
			landContract.StartBid();
			return landContract;
        }

        internal IList<LandContract> LandContractList => _landContracts.AsReadOnly();

		public bool FindLandContract(string blockId)
		{
			return _landContracts.Any(l => l.Id == blockId);
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain
{
	public class Player
	{
		private List<LandContract> landContracts = new();
		private int money;

        public PlayerState State { get; private set; } = PlayerState.Normal;
        public string Id { get; }
        public int Money => money;
		public Direction.Enumerates Direction { get; set; }

        public Player(string id)
		{
			Id = id;
		}

		public void SetState(PlayerState playerState)
		{
			State = playerState;
		}

		public void AddLandContract(LandContract landContract)
		{
			this.landContracts.Add(landContract);
		}

		public void AddMoney(int money)
		{
			this.money += money;
		}

		internal IList<LandContract> LandContractList => landContracts.AsReadOnly();
	}
}


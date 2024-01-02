using Domain.Models;

namespace Domain.Builders;

public class PlayerBuilder
{
	public string Id { get; set; }
	public decimal Money { get; set; }
	public string BlockId { get; set; }
	public Direction CurrentDirection { get; set; }
	public Map Map { get; set; } = default;
	public List<(string LandId, bool InMortgage, int Deadline)> LandContracts { get; set; }
	public PlayerState PlayerState { get; set; }
	public int BankruptRounds { get; private set; }
	public int RemainingSteps { get; set; }
	public int LocationId { get; set; }
	public string? RoleId { get; set; }

	public PlayerBuilder(string id)
	{
		Id = id;
		Money = 15000;
		BlockId = "Start";
		CurrentDirection = Direction.Right;
		LandContracts = new();
		PlayerState = PlayerState.Normal;
		RemainingSteps = 0;
		RoleId = null;
	}

	public PlayerBuilder WithMoney(decimal money)
	{
		Money = money;
		return this;
	}

	public PlayerBuilder WithPosition(string blockId, string direction)
	{
		BlockId = BlockId;
		CurrentDirection = Enum.Parse<Direction>(direction);
		return this;
	}

	public PlayerBuilder WithLandContract(string landId, bool inMortgage, int deadline)
	{
		LandContracts.Add(new(landId, inMortgage, deadline));
		return this;
	}

	public PlayerBuilder WithBankrupt(int Rounds)
	{
		if (Rounds > 0)
		{
			PlayerState = PlayerState.Bankrupt;
		}
		BankruptRounds = Rounds;
		return this; 
	}

	internal PlayerBuilder WithMap(Map map)
	{
		Map = map;
		return this;
	}

	public PlayerBuilder WithRemainingSteps(int remainingSteps)
	{
		RemainingSteps = remainingSteps;
		return this;
	}

	public PlayerBuilder WithLocation(int locationId)
	{
		LocationId = locationId;
		return this;
	}

	public PlayerBuilder WithRole(string? roleId)
	{
		RoleId = roleId;
		return this;
	}

	public PlayerBuilder WithState(PlayerState playerState = PlayerState.Normal)
	{
		PlayerState = playerState;
		return this;
	}

	public Player Build()
	{
		Player player = new(Id, Money, PlayerState, BankruptRounds, LocationId, RoleId);
		Chess chess = new(player, BlockId, CurrentDirection, RemainingSteps);
		player.Chess = chess;
		if (LandContracts.Count > 0 && Map == null)
		{
			throw new InvalidOperationException("Map must be set!");
		}
		Block block = Map.FindBlockById(BlockId);
		player.SuspendRound(block);
		foreach (var (LandId, InMortgage, DeadLine) in LandContracts)
		{
			var land = Map.FindBlockById<Land>(LandId);
			player.AddLandContract(new(player, land));
			if (InMortgage)
			{
				player.LandContractList[^1].GetMortgage();
				player.LandContractList[^1].SetDeadLine(DeadLine);
			}
		}
		return player;
	}
}

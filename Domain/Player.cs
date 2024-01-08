using Domain.Common;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;

namespace Domain;

public class Player
{
	private decimal _money;
	private readonly List<LandContract> _landContractList = new();
	
	public string Id { get; set; }
	public PlayerState State { get; internal set; }
	public decimal Money { get { return _money; } set { _money = Math.Max(0, value); } }
	public IList<LandContract> LandContractList => _landContractList.AsReadOnly();
	public Chess Chess { get; set; }
	public bool EndRoundFlag { get; set; }
	public int SuspendRounds { get; private set; } = 0;
	public int BankruptRounds { get; set; }
	public string? RoleId { get; set; }
	public int LocationId { get; set; }

	public Player(string id, decimal money = 15000, PlayerState playerState = PlayerState.Ready, int bankruptRounds = 0, int locationId = 0, string? roleId = null)
	{
		Id = id;
		_money = money;
		State = playerState;
		BankruptRounds = bankruptRounds;
		LocationId = locationId;
		RoleId = roleId;
	}

	internal DomainEvent UpdateState()
	{
		if (Money <= 0 && !LandContractList.Any(l => !l.InMortgage))
		{
			State = PlayerState.Bankrupt;
			foreach (var landContract in LandContractList)
			{
				RemoveLandContract(landContract);
			}
			EndRoundFlag = true;
			return new PlayerBankruptEvent(Id);
		}
		return DomainEvent.EmptyEvent;
	}

	public void AddLandContract(LandContract landContract)
	{
		_landContractList.Add(landContract);
		landContract.Land.UpdateOwner(this);
	}

	public void RemoveLandContract(LandContract landContract)
	{
		landContract.Land.UpdateOwner(null);
		_landContractList.Remove(landContract);
	}

	public LandContract? FindLandContract(string id)
	{
		return LandContractList.First(l => l.Land.Id == id);
	}

	public void SuspendRound(Block reason)
	{
		SuspendRounds = reason switch
		{
			Jail => 2,
			ParkingLot => 1,
			_ => 0
		};
	}

	public List<DomainEvent> EndRound()
	{
		List<DomainEvent> events = new();
		_landContractList.ForEach(l =>
		{
			events.Add(l.EndRound());
		});
		_landContractList.RemoveAll(l => l.DeadLine == 0);
		return events;
	}

	internal void StartRound()
	{
		EndRoundFlag = true;
		if (SuspendRounds > 0)
		{
			SuspendRounds--;
		}
	}

	internal IEnumerable<DomainEvent> RollDice(Map map, IDice[] dices)
	{
		foreach (var dice in dices)
		{
			dice.Roll();
		}
		yield return new PlayerRolledDiceEvent(Id, dices.Sum(d => d.Value));
		foreach (var e in Chess.Move(map, dices.Sum(dice => dice.Value)))
		{
			yield return e;
		}
    }
		
	internal void PayToll(Player owner, decimal amount)
	{
		Money -= amount;
		owner.Money += amount;
	}
	
}

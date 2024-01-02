namespace Domain;

public record CurrentPlayerState(
	string PlayerId,
	bool IsPayToll,
	bool IsBoughtLand,
	bool IsUpgreadeLand,
	Auction? Auction,
	int RemainingSteps,
	bool HasSelectedDirection)
{
	public bool CanEndRound => IsPayToll;
}

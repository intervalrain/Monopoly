namespace Domain;

public class Auction
{
	private readonly LandContract _landContract;
	private Player? _highestBidder;
	private decimal _highestPrice;

	public LandContract LandContract => _landContract;
	public Player? HighestBidder => _highestBidder;
	public decimal HighestPrice => _highestPrice;

	public Auction(LandContract landContract)
	{
		_landContract = landContract;
		_highestPrice = landContract.Land.GetPrice(LandUsage.Auction);
	}

	public Auction(LandContract landContract, Player highestBidder, decimal highestPrice)
	{
		_landContract = landContract;
		_highestBidder = highestBidder;
		_highestPrice = highestPrice;
	}
}

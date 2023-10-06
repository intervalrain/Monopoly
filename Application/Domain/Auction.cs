using Application;
using Application.Domain.Exceptions;

namespace Application.Domain;

public class Auction
{
	private LandContract landContract;
	private Player? highestBidder;
	private decimal highestPrice;

	public Auction(LandContract landContract)
	{
		this.landContract = landContract;
		highestPrice = landContract.Land.Price * (decimal)Resource.DEFAULT_BID_START;
	}

	/// <summary>
	/// 結算拍賣
	/// 將地契轉移給最高出價者
	/// 若流拍，將原有地契持有者的地契移除
	///
	/// 流拍金額為原土地價值的 70%
	/// </summary>
	internal void End()
	{
		Land land = landContract.Land;
		Player? owner = landContract.Owner; 
		if (highestBidder != null)
		{
            owner!.Money += highestPrice;
            highestBidder.AddLandContracts(land);
			highestBidder.Money -= highestPrice;
		}
		else // 流標
		{
            owner!.Money += (landContract.Land.Price * (decimal)Resource.DEFAULT_NO_BID);
        }
        owner!.RemoveLandContracts(land);
        land.UpdateOwner(highestBidder);
    }

	internal void Bid(Player player, int price)
	{
		if (price <= highestPrice)
		{
			throw new BidException($"出價要大於{highestPrice}");
		}
		else if (price > player.Money)
		{
			throw new BidException($"現金少於{price}");
		}
		highestPrice = price;
		highestBidder = player;
	}
}
namespace Application;

public static class Resource
{
	public const int DEFAULT_START_MONEY = 15000;
    public const float DEFAULT_BID_START = 0.5f;
    public const float DEFAULT_NO_BID = 0.7f;
	public const decimal DEFAULT_PASS_START_BONUS = 3000;
	public const decimal DEFAULT_LAND_PRICE = 1000;
	public const decimal DEFAULT_STATION_PRICE = 2000;
    public static decimal[] RATE_OF_LOT = new decimal[]
    {
        0, 1, 1.3m, 2, 4, 8, 16
    };
    public static decimal[] RATE_OF_HOUSE = new decimal[]
    {
        0.05m, 0.4m, 1, 3, 6, 10
    };
}

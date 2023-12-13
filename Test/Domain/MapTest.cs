using Domain;
using Domain.Maps;
using Domain.Interfaces;

namespace Test.Domain;

[TestClass]
public class MapTest
{
    [TestMethod]
    [Description(
    """
		Given: 給定地圖 Standard7x7
		 When: 
		 Then: 測試地圖格數為 30格
		""")]
    public void 檢查地圖總格數()
    {
        Map map = new(_7x7Map.Standard7x7);
		Assert.AreEqual(30, map.Blocks.Count());
    }

    [TestMethod]
	[Description(
		"""
		Given: 給定地圖 Standard7x7
		 When: 從 Start 開始走
		 Then: 可以正常走完 30 格
		""")]
	public void 檢查地圖每一塊都可以被走到()
	{
		Map map = new(_7x7Map.Standard7x7);
		IBlock start = map.FindBlockById("Start");
		var visited = new HashSet<string>();
		var stack = new Stack<IBlock>();
		stack.Push(start);
		while (stack.Any())
		{
			var curr = stack.Pop();
			visited.Add(curr.Id);
			foreach (IBlock next in curr.Neighbors)
			{
				if (visited.Contains(next.Id)) continue;
				stack.Push(next);
			}
		}
		Assert.AreEqual(30, visited.Count());
	}

    [TestMethod]
    [Description(
        """
		Given: 給定地圖 Standard7x7
		          | A4 |    
		       B6 | PL | C1 
		          | B1 |    
		 When: 位置在 ParkingLot
		 Then: 上為 A4，下為 B1，左為 B6，右為 C1
		""")]
    public void 檢查地圖相對關係()
    {
        Map map = new(_7x7Map.Standard7x7);
        IBlock curr = map.FindBlockById("ParkingLot");
        
        Assert.AreEqual("A4", curr.Up!.Id);
        Assert.AreEqual("B1", curr.Down!.Id);
        Assert.AreEqual("B6", curr.Left!.Id);
        Assert.AreEqual("C1", curr.Right!.Id);
    }
}

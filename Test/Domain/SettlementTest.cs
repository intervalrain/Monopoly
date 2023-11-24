using Shared.Domain;
using Shared.Domain.Maps;

namespace Test.Domain;

[TestClass]
public class SettlementTest
{
    [TestMethod]
    [Description(
        """
        Given: 玩家 ABC 三人
         When: B 先破產、C 後破產
         Then: 遊戲結算，排名 ACB
        """)]
    public void 玩家ABC_BC破產_遊戲結算_名次為ACB()
    {
        Game game = new();

        Player a = new("A");
        Player b = new("B");
        Player c = new("C");

        game.AddPlayers(a, b, c);

        game.AllocateMoney(b, -5000);
        game.AllocateMoney(c, -5000);

        var list = game.Settlement();
        
        Assert.AreEqual(list[0], a);
        Assert.AreEqual(list[1], c);
        Assert.AreEqual(list[2], b);
     }

    [TestMethod]
    [Description(
        """
        Given: 玩家 ABCD 四人
         When: 遊戲時間結束，
               A 有 5000 元
               B 有 4000 元
               C 有 3000 元
               D 有 2000 元
         Then: 遊戲結算，排名 ABCD
        """)]
    public void 玩家ABCD_遊戲時間結束_按照財產排名()
    {
        Game game = new();

        Player a = new("A", 5000);
        Player b = new("B", 4000);
        Player c = new("C", 3000);
        Player d = new("D", 2000);

        game.AddPlayers(a, b, c, d);

        var list = game.Settlement();

        Assert.AreEqual(list[0], a);
        Assert.AreEqual(list[1], b);
        Assert.AreEqual(list[2], c);
        Assert.AreEqual(list[3], d);
    }

    [TestMethod]
    [Description(
        """
        Given: 玩家 ABCD 四人
         When: 遊戲時間結束，
               A 有 2500 元，與空地 A1
               B 有 2000 元，與空地 B1
               C 有 2500 元
               D 有 2000 元
         Then: 遊戲結算，排名 ABCD
        """)]
    public void 玩家ABCD_遊戲時間結束_按照房產與財產排名()
    {
        Map map = new Map(_7x7Map.Standard7x7); 
        Game game = new(map);

        Player a = new("A", 2500);
        Player b = new("B", 2000);
        Player c = new("C", 2500);
        Player d = new("D", 2000);

        game.AddPlayers(a, b, c, d);

        var A1 = map.FindBlockById("A1");
        var B1 = map.FindBlockById("B1");
        A1.Contract.SetOwner(a);
        B1.Contract.SetOwner(b);

        var list = game.Settlement();

        Assert.AreEqual(list[0], a);
        Assert.AreEqual(list[1], b);
        Assert.AreEqual(list[2], c);
        Assert.AreEqual(list[3], d);
    }
}
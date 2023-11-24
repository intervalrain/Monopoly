using System;
using Shared.Domain;
using Shared.Domain.Enums;

namespace Test.Domain;

[TestClass]
public class UseMoneyTest
{


    [TestMethod]
    [Description(
        """
        Given: 玩家 ABCD 三人，各有財產 5000 元
         When: A 增加 3000元，B 花掉 3000元，C 花掉 5000 元，D 花掉 7000 元
         Then: A 現有 8000元，B 現有 2000元，C 現有 0元，D 現有 0元
        """)]
    public void 玩家ABCD_財產易動()
    {
        Game game = new();

        Player a = new("A");
        Player b = new("B");
        Player c = new("C");
        Player d = new("D");

        game.AddPlayers(a, b, c, d);

        game.AllocateMoney(a, +3000);
        game.AllocateMoney(b, -3000);
        game.AllocateMoney(c, -5000);
        game.AllocateMoney(d, -7000);

        Assert.AreEqual(8000, a.Money);
        Assert.AreEqual(2000, b.Money);
        Assert.AreEqual(0, c.Money);
        Assert.AreEqual(0, d.Money);
    }

    [TestMethod]
    [Description(
        """
        Given: 玩家 ABC 三人，各有財產 5000 元
         When: A 花掉 3000元，B 花掉 5000元，C 花掉 7000 元
         Then: B 破產、C破產
        """)]
    public void 玩家ABC_花錢_BC財產不足_BC破產()
    {
        Game game = new();

        Player a = new("A");
        Player b = new("B");
        Player c = new("C");

        game.AddPlayers(a, b, c);

        game.AllocateMoney(a, -3000);
        game.AllocateMoney(b, -5000);
        game.AllocateMoney(c, -7000);

        Assert.AreEqual(PlayerState.Normal, a.State);
        Assert.AreEqual(PlayerState.Bankrupt, b.State);
        Assert.AreEqual(PlayerState.Bankrupt, c.State);
    }
}

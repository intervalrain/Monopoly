using System;
using Shared.Domain;
using Shared.Domain.Enums;
using Shared.Domain.Maps;
using Shared.Interfaces;
using Test.Common;

namespace Test.Domain;

[TestClass]
public class RollDiceTest
{
	[TestMethod]
	[Description(
		"""
		Given: 玩家A目前在F4，面向上
		 When: 玩家擲到6點
		 Then: 玩家A移到到A4
		""")]
	public void 玩家擲骰後移動6步()
	{
		Map map = new Map(_7x7Map.Standard7x7);
		Game game = new Game("a001", map);
		var a = new Player("a");
		game.AddPlayer(a);
		game.SetPlayerToBlock(a, "F4", Direction.Up);

		IDice[]? dices = Utils.MockDice(2, 4);
		game.SetDice(dices);
		game.PlayerRollDice(a.Id);
		game.PlayerMoveChess();

		Assert.AreEqual("A4", game.GetPlayerPosition(a).Id);
	}
	[TestMethod]
    [Description(
        """
		Given: 玩家A目前在F3，持有1000元
		 When: 玩家擲到4點
		 Then: 玩家移動到A1，玩家持有4000元
		""")]
	public void 玩家擲骰後移動棋子經過起點獲獎勵金3000()
	{
		Map map = new Map(_7x7Map.Standard7x7);
		Game game = new Game("G01", map);
		var a = new Player("a", 1000);
		game.AddPlayer(a);
		game.SetPlayerToBlock(a, "F3", Direction.Up);
		IDice[]? dices = Utils.MockDice(4);
		game.SetDice(dices);
		
		game.PlayerRollDice(a.Id);
		game.PlayerMoveChess();

		Assert.AreEqual("A1", game.GetPlayerPosition(a).Id);
        Assert.AreEqual(0, game.CurrentDice);
        Assert.AreEqual(4000, a.Money);
	}
    [TestMethod]
    [Description(
        """
		Given: 玩家A目前在F3，持有1000元
		 When: 玩家擲到3點
		 Then: 玩家移動到Start，玩家持有1000元
		""")]
    public void 玩家擲骰後移動棋子到起點無法獲得獎勵金()
    {
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game("G01", map);
        var a = new Player("a", 1000);
        game.AddPlayer(a);
        game.SetPlayerToBlock(a, "F3", Direction.Up);
        IDice[]? dices = Utils.MockDice(3);
        game.SetDice(dices);

        game.PlayerRollDice(a.Id);
        game.PlayerMoveChess();

        Assert.AreEqual("Start", game.GetPlayerPosition(a).Id);
        Assert.AreEqual(0, game.CurrentDice);
        Assert.AreEqual(1000, a.Money);
    }
}

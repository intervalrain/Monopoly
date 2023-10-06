﻿using Application.Domain.Exceptions;
using Application.Domain;

namespace Test.Application.Domain;

[TestClass]
public class RollDiceTest
{
    [TestMethod]
    [Description(
        """
        Given:  目前玩家在F4
        When:   玩家擲骰得到6點
        Then:   A 移動到 A4
        """)]
    public void 玩家擲骰後移動棋子()
    {
        // Arrange
        var map = new Map(Utils.SevenXSevenMap);
        var game = new Monopoly("Test", map, Utils.MockDice(6));
        var player = new Player("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "F4", Direction.Up);

        // Act
        game.PlayerRollDice(player.Id);

        // Assert
        Assert.AreEqual("A4", game.GetPlayerPosition("A").Id);
    }

    [TestMethod]
    [Description(
        """
        Given:  目前玩家在F4
        When:   玩家擲骰得到7點
        Then:   A 移動到 停車場
                玩家需要選擇方向
        """)]
    public void 玩家擲骰後移動棋子到需要選擇方向的地方()
    {
        // Arrange
        var map = new Map(Utils.SevenXSevenMap);
        var game = new Monopoly("Test", map, Utils.MockDice(1, 6));
        var player = new Player("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "F4", Direction.Up);

        // Act
        Assert.ThrowsException<PlayerNeedToChooseDirectionException>(() => game.PlayerRollDice(player.Id));

        //Assert
        Assert.AreEqual("ParkingLot", game.GetPlayerPosition("A").Id);
    }

    [TestMethod]
    [Description(
    """
        Given:  目前玩家在F4
        When:   玩家擲骰得到8點
        Then:   A 移動到 停車場
                玩家需要選擇方向
                玩家剩餘步數為 1
        """)]
    public void 玩家擲骰後移動棋子到需要選擇方向的地方_剩餘1步()
    {
        // Arrange
        var map = new Map(Utils.SevenXSevenMap);
        var game = new Monopoly("Test", map, Utils.MockDice(2, 6));
        var player = new Player("A");
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "F4", Direction.Up);

        // Act
        Assert.ThrowsException<PlayerNeedToChooseDirectionException>(() => game.PlayerRollDice(player.Id));

        //Assert
        Assert.AreEqual("ParkingLot", game.GetPlayerPosition("A").Id);
        Assert.AreEqual(1, player.Chess.RemainingSteps);
    }

    [TestMethod]
    [Description(
    """
        Given:  目前玩家在F3
                玩家持有1000元
        When:   玩家擲骰得到4點
        Then:   玩家移動到 A1
                經過起點
                玩家剩餘步數為 0
                玩家持有4000元
        """)]
    public void 玩家擲骰後移動棋子經過起點獲得獎勵金3000()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Monopoly game = new Monopoly("Test", map, Utils.MockDice(2, 2));
        Player player = new("A", 1000);
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "F3", Direction.Up);

        // Act
        game.PlayerRollDice(player.Id);

        // Assert
        Assert.AreEqual("A1", game.GetPlayerPosition("A").Id);
        Assert.AreEqual(0, player.Chess.RemainingSteps);
        Assert.AreEqual(4000, player.Money);
    }

    [TestMethod]
    [Description(
    """
        Given:  目前玩家在F3
                玩家持有1000元
        When:   玩家擲骰得到3點
        Then:   玩家移動到 起點
                玩家剩餘步數為 0
                玩家持有1000元
        """)]
    public void 玩家擲骰後移動棋子停在起點無法獲得獎勵金()
    {
        // Arrange
        Map map = new Map(Utils.SevenXSevenMap);
        Monopoly game = new Monopoly("Test", map, Utils.MockDice(2, 1));
        Player player = new("A", 1000);
        game.AddPlayer(player);
        game.Initial();
        game.SetPlayerToBlock(player, "F3", Direction.Up);

        // Act
        game.PlayerRollDice(player.Id);

        // Assert
        Assert.AreEqual("Start", game.GetPlayerPosition("A").Id);
        Assert.AreEqual(0, player.Chess.RemainingSteps);
        Assert.AreEqual(1000, player.Money);
    }
}
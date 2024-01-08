using Domain.Builders;
using Domain.Events;
using Domain.Maps;
using Domain;
using Test.DomainTests;

namespace DomainTests.Testcases;

[TestClass]
public class RollDiceTest
{
    private static Map Map => new Domain.Maps.SevenXSevenMap();


    [TestMethod]
    [Description(
        """
        Given:  目前 A 在 F4，方向 Up
                A 持有 2000 元
        When:   A 擲骰得到 6 點
        Then:   A 移動到 A4，方向 Down
                A 獲得 3000 元，還有 5000 元
                A 剩餘步數為 0

                DomainEvent:    1. A 擲骰子得到了 6 點
                                2. A 移動到 Start，方向 Up，剩餘 5 步
                                3. A 獲得 3000 元，還有 5000 元
                                4. A 移動到 A1，方向 Right，剩餘 4 步
                                5. A 移動到 Station1，方向 Right，剩餘 3 步
                                6. A 移動到 A2，方向 Right，剩餘 2 步
                                7. A 移動到 A3，方向 Right，剩餘 1 步
                                8. A 移動到 A4，方向 Down，剩餘 0 步
                                9. A 可以買下 A4，價錢 1000
        
        """)]
    public void 玩家擲骰後移動棋子經過起點獲得獎勵金()
    {
        // Arrange
        var a = new
        {
            Id = "A",
            CurrentBlockId = "F4",
            CurrentDirection = Direction.Up,
            Money = 2000m
        };
        int dice = 6;
        var expected = new
        {
            BlockId = "A4",
            Direction = Direction.Down,
            RemainingSteps = 0,
            Money = 5000m 
        };

        var monopoly = new MonopolyBuilder()
            .WithMap(Map)
            .WithPlayer(a.Id, p => p.WithMoney(a.Money).
                                     WithPosition(a.CurrentBlockId, a.CurrentDirection))
            .WithDices(Utils.MockDice(dice))
            .WithCurrentPlayer(a.Id)
            .Build();

        // Act
        monopoly.PlayerRollDice(a.Id);

        // Assert
        var player = monopoly.Players.First(p => p.Id == a.Id);
        var blockId = player.Chess.CurrentBlockId;
        var direction = player.Chess.CurrentDirection;
        var remainingSteps = player.Chess.RemainingSteps;
        var money = player.Money;

        Assert.AreEqual(blockId, expected.BlockId);
        Assert.AreEqual(direction, expected.Direction);
        Assert.AreEqual(remainingSteps, expected.RemainingSteps);
        Assert.AreEqual(money, expected.Money);

        monopoly.DomainEvents
        .NextShouldBe(new PlayerRolledDiceEvent(a.Id, dice))
        .NextShouldBe(new ThroughStartEvent(a.Id, 3000m, 5000m))
        .NextShouldBe(new ChessMovedEvent(a.Id, "Start", Direction.Right, 5))
        .NextShouldBe(new ChessMovedEvent(a.Id, "A1", Direction.Right, 4))
        .NextShouldBe(new ChessMovedEvent(a.Id, "Station1", Direction.Right, 3))
        .NextShouldBe(new ChessMovedEvent(a.Id, "A2", Direction.Right, 2))
        .NextShouldBe(new ChessMovedEvent(a.Id, "A3", Direction.Down, 1))
        .NextShouldBe(new ChessMovedEvent(a.Id, "A4", Direction.Down, 0))
        .NextShouldBe(new PlayerCanBuyLandEvent(a.Id, "A4", 1000m))
        .NoMore();


    }
}

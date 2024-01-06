using Domain;
using Domain.Builders;
using Domain.Events;
using Domain.Maps;

namespace Test.DomainTests.Testcases;

[TestClass]
public class SettlementTest
{
    private static Map Map => new SevenXSevenMap();

    [TestMethod]
    [Description(
        """
        Given:  遊戲目前第 7 回合
                A 持有 5000 元
                B 第 6 回合破產
                C 第 7 回合破產
        When:   系統進行遊戲結算
        Then:   A 第一名
                B 第三名
                C 第二名

                DomainEvent:    遊戲結束，7 回合，名次為 A C B，A 剩餘 5000 元，C 第 7 回合破產，B 第 6 回合破產
        """)]
    public void 因為有人破產而進行遊戲結算()
    {
        // Arrange
        var a = new Player(id: "A", money: 5000);
        var b = new Player(id: "B", money: 0, bankruptRounds: 6);
        var c = new Player(id: "C", money: 0, bankruptRounds: 7);
        var expect = new { Rounds = 7 };

        var monopoly = new MonopolyBuilder()
            .WithRounds(7)
            .WithMap(Map)
            .WithPlayer(a.Id, p => p.WithMoney(a.Money))
            .WithPlayer(b.Id, q => q.WithMoney(b.Money).WithBankrupt(b.BankruptRounds))
            .WithPlayer(c.Id, r => r.WithMoney(c.Money).WithBankrupt(c.BankruptRounds))
            .WithCurrentPlayer(a.Id)
            .Build();

        // Act
        monopoly.Settlement();
        var A = monopoly.Players.First(p => p.Id == a.Id);
        var B = monopoly.Players.First(p => p.Id == b.Id);
        var C = monopoly.Players.First(p => p.Id == c.Id);

        // Assert
        monopoly.DomainEvents.NextShouldBe(new GameSettlementEvent(expect.Rounds, A, C, B));
    }
}
using Application.Common;
using Domain;
using Domain.Events;
using Domain.Maps;
using Microsoft.AspNetCore.SignalR.Client;
using Test.Common;

namespace Test.Usecases;

[TestClass]
public class RollDiceUsecaseTest
{
    private TestServer server;

    [TestInitialize]
    public void Setup()
    {
        server = new TestServer();
    }

    [TestMethod]
    [Ignore]
    [Description(
        """
        Given: 目前玩家在 F4
         When: 玩家擲骰得到 7 點
         Then: A 移動到 A4
        """)]
    public async Task 玩家擲骰後移動棋子()
    {
        //// Arrange
        //SetupGame("1", 1, 7);
        //var hub = server.CreateHubConnection();
        //var verifyPlayerRollDiceEvent = hub.Verify<PlayerRollDiceEvent>("PlayerRollDiceEvent");
        //var verifyChessMoveEvent = hub.Verify<ChessMoveEvent>("ChessMoveEvent");

        //// Act
        //await hub.SendAsync("PlayerRollDice", "1", "A");

        //// Assert
        //await verifyPlayerRollDiceEvent.Verify(e => e.GameId == "1"
        //                                            && e.PlayerId == "A"
        //                                            && e.DiceCount == 7);
        //await verifyChessMoveEvent.Verify(e => e.GameId == "1"
        //                                       && e.PlayerId == "A"
        //                                       && e.BlockId == "A4");
        
        
    }

    private void SetupGame(string GameId, int playerNo, params int[] dices)
    {
        var repo = server.GetRequiredService<IRepository>();
        Map map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(GameId, map);
        game.SetDice(Utils.MockDice(dices));
        var players = new Player[playerNo];
        for (int i = 0; i < playerNo; i++)
        {
            players[i] = new Player("A" + i);
        }
        game.AddPlayers(players);
        repo.Save(game);
    }
}
using Application.Common;
using Domain.Events;
using Microsoft.AspNetCore.SignalR.Client;
using Test.Common;

namespace Test.Usecases;

[TestClass]
public class CreateGameUsecaseTest
{
    private TestServer server;
    private IRepository repository;

    [TestInitialize]
    public void Setup()
    {
        server = new TestServer();
        repository = server.GetRequiredService<IRepository>();
    }
    [TestMethod]
    public async void 初始化遊戲_共四人遊戲()
    {
        // Arrange
        var hub = server.CreateHubConnection();
        var verification = hub.Verify<GameCreatedEvent>("GameCreatedEvent", Timeout: 5000);

        // Act
        await hub.SendAsync("CreateGame", "a");

        // Assert
        await verification.Verify(e => e.GameId == "1");

    }
}

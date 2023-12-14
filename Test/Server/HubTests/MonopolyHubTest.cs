using Application.Usecases;
using Moq;
using Server.Hubs;
using SignalR_UnitTestingSupportCommon.Hubs;

namespace Test.Server.HubTests;

[TestClass]
public class MonopolyHubTest
{
    [TestMethod]
    [Ignore]
    public async Task TestMethodAsync()
    {
        //// Arrange
        //var support = new HubUnitTestsSupport();
        //support.SetUp();
        //var hub = new MonopolyHub();
        //support.AssignToHubRequiredProperties(hub);

        //// Act
        //await hub.SendMessage("user", "message");

        //// Assert
        //support
        //.ClientsAllMock
        //.Verify(
        //    x => x.SendCoreAsync(
        //        "ReceiveMessage",
        //        new object[] { "user", "message" },
        //        It.IsAny<CancellationToken>())
        //);
    }

    [TestMethod]
    [Ignore]
    public async Task 輪到玩家A_玩家A在起點__玩家擲骰子得到6__所有玩家都接收到玩家A所擲的骰子點數6()
    {
    //    // Arrange
    //    var support = new HubUnitTestsSupport();
    //    support.SetUp();
    //    var hub = new MonopolyHub();
    //    support.AssignToHubRequiredProperties(hub);

    //    // Act
    //    await hub.PlayerRollDice("g1", "p1", null);

    //    // Assert
    //    support
    //    .ClientsGroupMock
    //    .Verify(
    //        x => x.SendCoreAsync(
    //            "PlayerRollDice",
    //            new object[] { "p1", 6 },
    //            It.IsAny<CancellationToken>())
    //    );
    }
}
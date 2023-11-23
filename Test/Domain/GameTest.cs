using Shared.Domain;

namespace Shared.Usecases;

[TestClass]
public class GameTest
{
    [TestMethod]
    public void SettlementTest()
    {
        Game game = new();

        string[] ids = new string[] { "a", "b", "c" };

        game.AddPlayers("a", "b", "c");

        game.SetState("b", PlayerState.Bankrupt);
        game.SetState("c", PlayerState.Bankrupt);

        Player? winner = game.Settlement();
        Player? a = game.FindPlayerById("a");
        Assert.AreEqual(winner, a);
    }
}
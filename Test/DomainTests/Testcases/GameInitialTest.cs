using Domain;
using Domain.Maps;

namespace Test.DomainTests;

[TestClass]
public class GameInitialTest
{
    private static Map Map => new SevenXSevenMap();

    [TestMethod]
    public void 玩家ABCD__遊戲初始設定__每位玩家有15000_每位玩家都會在起點()
    {
        // Arrange
        var a = new Player("A");
        var b = new Player("B");
        var c = new Player("C");
        var d = new Player("D");

        var map = Map;
    }
}
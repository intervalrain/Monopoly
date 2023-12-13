using Domain.Maps;
using Domain;
using Domain.Enums;

namespace Test.Domain;

[TestClass]
public class MoveChessTest
{
	[TestMethod]
	[Description(
		"""
		Given: 玩家目前在 Start，面向左
		 When: 擲骰子擲到 6 點
		 Then: 玩家移動到 Prison
		""")]
	public void 玩家擲到6點_目前在F4_移動到Prison()
	{
		Map map = new Map(_7x7Map.Standard7x7); 
		Game game = new Game("002", map);
		var a = new Player("A");

		game.AddPlayer(a);
		game.SetPlayerToBlock(a, "F4", Direction.Up);
		game.MovePlayer(a, 6);

		Assert.AreEqual(a.Position!.Id, "A4");

	}
}

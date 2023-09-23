using System;
using Shared.Domain;
using Shared.Usecases;
using Test.Shared.Utils;

namespace Test.Shared.Usecases
{
	[TestClass]
	public class MoveChessUsecasesTest
	{
		[TestMethod]
		public void MoveChessTest()
		{
			// it's a's round. a is at Start, roll dice as 5. Move chess to A4.
			const string GameId = "g1";
			const string a = "A";
            const string b = "B";
            const string c = "C";
            const string d = "D";

			Game game = UsecaseUtils.GameSetup(GameId, new DiceSetting(1, 5, 5),
				a, b, c, d);
			game.PlayerRollDice(a);
			new JsonRepository().Save(game);

			MoveChessUsecase.Input input = new(GameId, a);
			MoveChessUsecase.Presenter presenter = new MoveChessUsecase.Presenter();

			MoveChessUsecase moveChessUsecase = new MoveChessUsecase(new JsonRepository());
			moveChessUsecase.Execute(input, presenter);

			Assert.IsTrue("A4".Equals(presenter.ChessPosition) || "F1".Equals(presenter.ChessPosition));
		}
	}
}


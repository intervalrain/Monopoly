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
			const string PlayerA = "A";
            const string PlayerB = "B";
            const string PlayerC = "C";
            const string PlayerD = "D";

            UsecaseUtils.GameSetup(GameId, PlayerA, PlayerB, PlayerC, PlayerD);
			UsecaseUtils.SetGameDice(GameId, 5);
			MoveChessUsecase.Input input = new(GameId, PlayerA);
			MoveChessUsecase.Presenter presenter = new MoveChessUsecase.Presenter();

			MoveChessUsecase moveChessUsecase = new MoveChessUsecase(new JsonRepository());
			moveChessUsecase.Execute(input, presenter);

			Assert.IsTrue("A4".Equals(presenter.ChessPosition) || "F1".Equals(presenter.ChessPosition));
		}
	}
}


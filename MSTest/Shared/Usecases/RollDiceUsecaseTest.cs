using System;
using Test.Shared.Utils;
using Shared.Usecases;
using Shared.Domain;

namespace Test.Shared.Usecases
{
	[TestClass]
	public class RollDiceUsecaseTest
	{
		[TestMethod]
		public void RollDiceTest()
		{
			// player a's round, a roll dice at the start.
			const string GameId = "g2";
			const string PlayerA = "A";
            const string PlayerB = "B";
            const string PlayerC = "C";
            const string PlayerD = "D";

            UsecaseUtils.GameSetup(GameId, PlayerA, PlayerB, PlayerC, PlayerD);
            RollDiceUsecase.Input input = new(GameId, PlayerA);
			RollDiceUsecase.Presenter presenter = new RollDiceUsecase.Presenter();

			RollDiceUsecase rollDiceUsecase = new RollDiceUsecase(new JsonRepository());
			rollDiceUsecase.Execute(input, presenter);	
		}
	}
}


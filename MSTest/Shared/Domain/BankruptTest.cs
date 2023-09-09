using System;
using Shared.Domain;

namespace Test.Shared.Domain
{
	[TestClass]
	public class BankruptTest
	{
		[TestMethod]
		public void LackOfHouseAndMoney()
		{
			Player playerA = new("A");
			playerA.SetState(PlayerState.Bankrupt);

			Assert.AreEqual(playerA.State, PlayerState.Bankrupt);
		}
	}
}


using System;
using Shared.Domain;

namespace Test.Shared.Utils
{
	public class UsecaseUtils
	{
        public static Game GameSetup(string gameId, params string[] playerIds)
        {
            return GameSetup(gameId, null, playerIds);
        }

        public static Game GameSetup(string gameId, DiceSetting diceSetting = null, params string[] playerIds)
        {
            Map map = new(Utils._7x7Map);
            Game game = new(gameId, map, diceSetting);
            int n = playerIds.Length;
            Player[] players = playerIds.Select(p => new Player(p)).ToArray();
            Player a = new("A");
            Player b = new("B");
            Player c = new("C");
            Player d = new("D");

            game.AddPlayers(players);
            game.Initial();

            var gameRepository = new JsonRepository();
            gameRepository.Save(game);
            return game;
        }

        public static Game GetGameById(string id)
        {
            var gameRepository = new JsonRepository();
            return gameRepository.FindGameById(id);
        }

        public static void SetGameDice(string id, int dice)
        {
            var gameRepository = new JsonRepository();
            var game = gameRepository.FindGameById(id);
            game.CurrentDice = dice;
            gameRepository.Save(game);
        }
    }
}


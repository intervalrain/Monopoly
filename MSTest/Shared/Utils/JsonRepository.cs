using System.Text.Json;
using Shared.Domain;
using Shared.Repositories;

namespace Test.Shared.Utils
{
	public class JsonRepository : IRepository
	{
		public Game FindGameById(string id)
		{
			string str = File.ReadAllText($"./{id}.json");
			var jGame = JsonSerializer.Deserialize<JsonGame>(str);

            Map map = new(MapLibrary._7x7Map);
            Game game = new(jGame.Id, map);
            foreach (JsonPlayer jPlayer in jGame.Players)
            {
                game.AddPlayer(new(jPlayer.Id));
                game.SetPlayerToBlock(game.Players.Last(), jPlayer.CurrentBlockId,
                    (Direction.Enumerates)Enum.Parse(typeof(Direction.Enumerates), jPlayer.CurrentDirection));
            }
            game.CurrentPlayer = jGame.CurrentPlayer == null ? null : game.Players.First(p => p.Id == jGame.CurrentPlayer.Id);
            game.CurrentDice = jGame.CurrentDice;
            return game;
		}

        public void Save(Game game)
        {
            Player? currentPlayer = game.CurrentPlayer;
            if (currentPlayer == null) return;
            JsonGame jGame = new()
            {
                Id = game.Id,
                CurrentDice = game.CurrentDice,
                CurrentPlayer = new()
                {
                    Id = currentPlayer.Id,
                    CurrentBlockId = game.GetPlayerPosition(currentPlayer),
                    CurrentDirection = game.GetPlayerDirection(currentPlayer).ToString()
                },
                Players = game.Players.Select(p => new JsonPlayer
                {
                    Id = p.Id,
                    CurrentBlockId = game.GetPlayerPosition(p),
                    CurrentDirection = game.GetPlayerDirection(p).ToString()
                }).ToList()
            };
            string text = JsonSerializer.Serialize(jGame);
            File.WriteAllText($"./{game.Id}.json", text);
        }


        private class JsonGame
        {
            public string Id { get; set; }
            public int CurrentDice { get; set; }
            public JsonPlayer CurrentPlayer { get; set; }
            public List<JsonPlayer> Players { get; set; }
        }

        private class JsonPlayer
        {
            public string Id { get; set; }
            public string CurrentBlockId { get; set; }
            public string CurrentDirection { get; set; }
        }
    }
}


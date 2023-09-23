using System.Text.Json;
using Shared.Domain;
using Shared.Repositories;
using static Shared.Domain.Map;

namespace Test.Shared.Utils
{
	public class JsonRepository : IRepository
	{
		public Game FindGameById(string id)
		{
			string stringGame = File.ReadAllText($"./{id}.json");
			var jsonGame = JsonSerializer.Deserialize<JsonGame>(stringGame);

            if (jsonGame == null)
            {
                throw new FormatException($"Invalid Json Game (id:{id})");
            }

            Map map = new Map(Utils._7x7Map);
            Game game = new(jsonGame.Id, map, new DiceSetting
            {
                NumberOfDice = jsonGame.DiceSetting.NumberOfDice,
                Max = jsonGame.DiceSetting.Max,
                Min = jsonGame.DiceSetting.Min
            });

            foreach (JsonPlayer jsonPlayer in jsonGame.Players)
            {
                var player = new Player(jsonPlayer.Id);
                game.AddPlayer(player);
                game.SetPlayerToBlock(player, jsonPlayer.CurrentBlockId,
                    (Direction)Enum.Parse(typeof(Direction), jsonPlayer.CurrentDirection));
            }
            game.CurrentPlayerId = game.Players.Where(p => p.Id == jsonGame.CurrentPlayerId).FirstOrDefault().Id;
            game.CurrentDice = jsonGame.CurrentDice;
            return game;
		}

        public void Save(Game game)
        {
            Player currentPlayer = game.FindPlayerById(game.CurrentPlayerId);
            if (currentPlayer == null) return;
            JsonGame jGame = new()
            {
                Id = game.Id,
                CurrentDice = game.CurrentDice,
                CurrentPlayerId = currentPlayer.Id,
                Players = game.Players.Select(p => new JsonPlayer
                {
                    Id = p.Id,
                    CurrentBlockId = game.GetPlayerPosition(p),
                    CurrentDirection = game.GetPlayerDirection(p).ToString()
                }).ToList(),
                DiceSetting = new()
                {
                    Max = game.DiceSetting.Max,
                    Min = game.DiceSetting.Min,
                    NumberOfDice = game.DiceSetting.NumberOfDice
                }
            };
            string text = JsonSerializer.Serialize(jGame);
            File.WriteAllText($"./{game.Id}.json", text);
        }


        private class JsonGame
        {
            public required string Id { get; set; }
            public int CurrentDice { get; set; }
            public required string CurrentPlayerId { get; set; }
            public List<JsonPlayer> Players { get; set; }
            public required JsonDiceSetting DiceSetting  { get;set;}
        }

        private class JsonDiceSetting
        {
            public required int Max { get; set; }
            public required int Min { get; set; }
            public required int NumberOfDice { get; set; }
        }

        private class JsonPlayer
        {
            public required string Id { get; set; }
            public required string CurrentBlockId { get; set; }
            public required string CurrentDirection { get; set; }
        }
    }
}


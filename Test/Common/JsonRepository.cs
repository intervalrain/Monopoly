using System.Text.Json;
using Shared.Domain;
using Shared.Domain.Enums;
using Shared.Domain.Maps;
using Shared.Repositories;

namespace Test.Common;

public class JsonRepository : IRepository
{
    public Game FindGameById(string id)
    {
        string text = File.ReadAllText($"./{id}.json");
        var jsonGame = JsonSerializer.Deserialize<JsonGame>(text);

        var map = new Map(_7x7Map.Standard7x7);
        Game game = new Game(jsonGame.Id, map);
        foreach (var p in jsonGame.Players)
        {
            var player = new Player(p.Id, p.Money);
            player.DiceNum = p.DiceNum;
            game.AddPlayer(player);
            game.SetPlayerToBlock(player, p.Position, (Direction)p.Direction);
        }
        game.CurrentDice = jsonGame.CurrentDice;
        game.CurrentPlayer = game.GetPlayerById(jsonGame.CurrentPlayer);
        return game;
    }

    public void Save(Game game)
    {
        List<JsonPlayer> players = game.Players.Select(p => new JsonPlayer()
        {
            Id = p.Id,
            Money = p.Money, 
            Direction = (int)p.Direction,
            Position = p.Position.Id,
            DiceNum = p.DiceNum
        }).ToList();
        var currPlayer = players.First(p => p.Id == game.CurrentPlayer.Id);
        JsonGame jsonGame = new()
        {
            Id = game.Id,
            CurrentDice = game.CurrentDice,
            CurrentPlayer = currPlayer.Id,
            Players = players
        };
        string text = JsonSerializer.Serialize(jsonGame);
        File.WriteAllText($"./{game.Id}.json", text);
    }

    internal class JsonGame
    {
        public string Id { get; set; }
        public int CurrentDice { get; set; }
        public string CurrentPlayer { get; set; }
        public List<JsonPlayer> Players { get; set; }
    }

    internal class JsonPlayer
    {
        public string Id { get; set; }
        public int Money { get; set; }
        public int Direction { get; set; }
        public string Position { get; set; }
        public int DiceNum { get; set; }
    }
}

using Server.Repositories;
using Application.Domain;
using Application.Domain.Interfaces;

namespace Test.Application.Usecases.Utils;

public class UsecaseUtils
{
    public static Monopoly GameSetup(IDice[]? dice = null)
    {
        var map = new Map(Application.Utils.SevenXSevenMap);
        var game = new Monopoly("g1", map, dice);
        var playerA = new Player("p1");
        var playerB = new Player("p2");
        var playerC = new Player("p3");
        var playerD = new Player("p4");

        game.AddPlayer(playerA);
        game.AddPlayer(playerB);
        game.AddPlayer(playerC);
        game.AddPlayer(playerD);

        game.Initial();

        var gameRepository = new InMemoryRepository();
        gameRepository.Save(game);

        return game;
    }

    public static Monopoly GetGameById(string id)
    {
        var gameRepository = new InMemoryRepository();
        return gameRepository.FindGameById(id);
    }
}

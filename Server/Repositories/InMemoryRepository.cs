﻿using System;
using Application.Domain;
using Application.Common;

namespace Server.Repositories;

public class InMemoryRepository : IRepository
{
    private static readonly Dictionary<string, Monopoly> Games = new(); 

    public Monopoly FindGameById(string id)
    {
        Games.TryGetValue(id, out Monopoly? game);
        if (game == null)
        {
            throw new GameNotFoundException(id);
        }
        return game;
    }

    public string Save(Monopoly game)
    {
        game.Id ??= (Games.Count + 1).ToString(); 
        Games[game.Id] = game;
        return game.Id;
    }
}



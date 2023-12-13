﻿using Domain.Repositories;

namespace Domain.Usecases;

public class RollDiceUsecase
{
    private readonly IRepository _repository;

    public RollDiceUsecase(IRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Input input, Presenter presenter)
    {
        // 查
        var game = _repository.FindGameById(input.GameId);
        // 改
        game.PlayerRollDice();
        // 存
        _repository.Save(game);
        // 推
        presenter.Dice = game.CurrentDice;
    }

    public record Input(string GameId, string PlayerId);

    public class Presenter
    {
        public int Dice { get; set; }
    }

}

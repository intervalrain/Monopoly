using System;
using Shared.Domain;
using Shared.Repositories;

namespace Shared.Usecases;

public class CreateGameUsecase
{
    private readonly IRepository _repository;

    public CreateGameUsecase(IRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Input input, Presenter presenter)
    {
        // 查
        Game game = Game.Create(input.GameId, input.PlayerIds);
        // 改
        // 存
        _repository.Save(game);
        // 推
        presenter.CurrentTime = DateTime.Now;
        presenter.GameId = input.GameId;
        presenter.PlayerNum = input.PlayerIds.Length;
        presenter.PlayerNames = input.PlayerIds.ToList();
    }

    public record Input(string GameId, string[] PlayerIds);

    public class Presenter
    {
        public DateTime CurrentTime { get; set; }
        public string GameId { get; set; }
        public int PlayerNum { get; set; }
        public List<string> PlayerNames { get; set; } 

    }
}

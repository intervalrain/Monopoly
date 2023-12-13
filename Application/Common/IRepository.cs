using Domain;

namespace Application.Common;

public interface IRepository
{
    public Game FindGameById(string id);

    public string Save(Game game);
}
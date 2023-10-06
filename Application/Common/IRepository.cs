using Application.Domain;

namespace Application.Common;

public interface IRepository
{
    public Monopoly FindGameById(string id);

    public string Save(Monopoly game);
}
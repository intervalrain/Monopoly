using Shared.Domain;
using Shared.Domain.Enums;

namespace Shared.Interfaces;

public interface IBlock
{
    string Id { get; }
    Facility Facility { get; }
    IBlock? Up { get; }
    IBlock? Down { get; }
    IBlock? Left { get; } 
    IBlock? Right { get; }
    IBlock[] Neighbors { get; }
    Contract Contract { get; } 

    void SetConnection(Direction dir, IBlock block);
    IBlock Next(Direction dir);
    Direction GetRelation(IBlock block);
}

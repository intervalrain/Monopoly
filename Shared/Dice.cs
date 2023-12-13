using Domain.Interfaces;

namespace Domain;

public class Dice : IDice
{
    public int Value { get; private set; }

    public void Roll()
    {
        Value = new Random().Next(1, 6);
    }
}

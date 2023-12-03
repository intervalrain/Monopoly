using System;
namespace Shared.Interfaces;

public interface IDice
{
	public int Value { get; }
	public void Roll();
}

using kakuro;

namespace Kakuro;

public abstract class Tile(int x, int y) : IPrintable
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;

    public abstract string Print();
}
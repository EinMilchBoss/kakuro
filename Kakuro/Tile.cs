namespace Kakuro;

public abstract class Tile(int x, int y)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
}
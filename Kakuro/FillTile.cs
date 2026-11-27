namespace Kakuro;

public class FillTile(int x, int y, int value = 0) : Tile(x, y)
{
    public int Value { get; set; } = value;

    public override string Print()
    {
        return $"  {Value}  ";
    }
}
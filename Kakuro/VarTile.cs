namespace Kakuro;

public class VarTile(int x, int y, int value = 0) : Tile(x, y)
{
    public int Value { get; set; } = value;
    private readonly List<TileValuePossibilities> _possibilities = [];

    public override string Print()
    {
        return $"  {Value}  ";
    }
}
namespace Kakuro;

public class Grid
{
    private readonly Tile[,] _tiles;
    public Tile this[int y, int x]
    {
        get => _tiles[y, x];
        set => _tiles[y, x] = value;
    }

    public int Dimensions { get; init; }

    public Grid(int dimensions)
    {
        Dimensions = dimensions;
        _tiles = new Tile[Dimensions, Dimensions];
    }
}
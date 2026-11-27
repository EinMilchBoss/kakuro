namespace Kakuro;

public class Grid
{
    private readonly Tile[,] _tiles;
    public Tile this[int y, int x]
    {
        get => _tiles[y, x];
    }

    public int Dimensions { get; init; }
    public IReadOnlyList<Constraint> Constraints { get; init; }

    private Grid(Tile[,] tiles, int dimensions, IReadOnlyList<Constraint> constraints)
    {
        _tiles = tiles;
        Dimensions = dimensions;
        Constraints = constraints;
    }

    public static Grid FromSetTiles(int dimensions, IEnumerable<SetTile> setTiles)
    {
        Tile[,] tiles = CreateTiles(dimensions, setTiles);

        List<Constraint> constraints = [];
        foreach (SetTile setTile in setTiles)
        {
            if (setTile.RightSum != 0)
                constraints.Add(CreateRightConstraint(tiles, dimensions, setTile));
            if (setTile.DownSum != 0)
                constraints.Add(CreateRightConstraint(tiles, dimensions, setTile));
        }

        return new(tiles, dimensions, constraints);
    }

    private static Tile[,] CreateTiles(int dimensions, IEnumerable<SetTile> setTiles)
    {
        Tile[,] tiles = new Tile[dimensions, dimensions];

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                tiles[y, x] = new VarTile(x, y);
            }
        }
        foreach (SetTile setTile in setTiles)
            tiles[setTile.Y, setTile.X] = setTile;

        return tiles;
    }

    private static Constraint CreateRightConstraint(Tile[,] tiles, int dimensions, SetTile origin) =>
        CreateConstraint(
            dimensions,
            origin,
            origin.X,
            i => tiles[origin.Y, origin.X + i]
        );

    private static Constraint CreateDownConstraint(Tile[,] tiles, int dimensions, SetTile origin) =>
        CreateConstraint(
            dimensions,
            origin,
            origin.Y,
            i => tiles[origin.Y + i, origin.X]
        );

    private static Constraint CreateConstraint(
        int dimensions,
        SetTile origin,
        int originComponent,
        Func<int, Tile> advance
    )
    {
        List<VarTile> varTiles = [];

        int limit = dimensions - originComponent;
        for (int i = 1; i < limit; i++)
        {
            Tile tile = advance(i);
            if (tile is not VarTile varTile)
                break;

            varTiles.Add(varTile);
        }

        return new(origin.DownSum, varTiles);
    }
}
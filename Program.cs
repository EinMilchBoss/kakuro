GridConverter converter = new();
var result = converter.ParseFromImport("test.kkr", out var grid);
if (!result.IsSuccess)
{
    Console.Error.WriteLine("Error!");
    Console.Error.WriteLine(string.Join(Environment.NewLine, result.Problems));
    return 1;
}

for (int y = 0; y < grid.Dimensions; y++)
{
    Console.Write('|');
    for (int x = 0; x < grid.Dimensions; x++)
    {
        Console.Write($"{grid[y, x]}|");
    }
    Console.WriteLine();
}

return 0;

class GridConvertResult
{
    public bool IsSuccess => Problems.Count == 0;
    public ICollection<string> Problems { get; init; } = [];

    public void AddProblem(int lineNumber, string problem)
    {
        Problems.Add($"Line {lineNumber}: {problem}");
    }
}

class GridConverter
{
    private const int PartsPerLine = 4;
    private const int MinDimensions = 5;

    public GridConvertResult ParseFromImport(string path, out Grid grid)
    {
        grid = default!;

        GridConvertResult result = new();

        string content = File.ReadAllText(path);
        string[] lines = content.Split(Environment.NewLine);

        if (!int.TryParse(lines[0], out int dimensions) && dimensions >= MinDimensions)
            result.AddProblem(1, $"The first line doesn't contain an integer for the dimensions of at least {MinDimensions}.");

        grid = new(Math.Max(dimensions, MinDimensions));

        Span<int> lineValues = stackalloc int[PartsPerLine];
        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;
            string line = lines[i];

            string[] parts = line.Split(',');
            if (parts.Length != PartsPerLine)
            {
                result.AddProblem(lineNumber, "Does not contain exactly 4 numbers for y, x, downSum, rightSum.");
                continue;
            }

            for (int j = 0; j < PartsPerLine; j++)
            {
                int partNumber = j + 1;

                if (!int.TryParse(parts[j], out var parsed))
                {
                    result.AddProblem(lineNumber, $"Part {partNumber} is not a valid integer.");
                    continue;
                }

                lineValues[j] = parsed;
            }

            int y = lineValues[0];
            int x = lineValues[1];
            int downSum = lineValues[2];
            int rightSum = lineValues[3];

            grid[y, x] = new SetTile(x, y, rightSum, downSum);
        }

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                if (grid[y, x] == null)
                    grid[y, x] = new FillTile(x, y);
            }
        }

        return result;
    }
}

class Grid
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

abstract class Tile(int x, int y)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
}

class SetTile(int x, int y, int rightSum, int downSum) : Tile(x, y)
{
    public int RightSum { get; } = rightSum;
    public int DownSum { get; } = downSum;

    public bool IsEmpty { get; } = rightSum == 0 && downSum == 0;

    public override string ToString()
    {
        return $"{DownSum,2}\\{RightSum,-2}";
    }
}

class FillTile(int x, int y, int value = 0) : Tile(x, y)
{
    public int Value { get; set; } = value;

    public override string ToString()
    {
        return $"{Value,-5}";
    }
}
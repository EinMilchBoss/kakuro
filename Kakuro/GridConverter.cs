namespace Kakuro;

public class GridConverter
{
    private const int PartsPerLine = 4;
    private const int MinDimensions = 5;

    public GridConversionResult ParseFromImport(string path, out Grid grid)
    {
        grid = default!;

        List<string> problems = [];

        string content = File.ReadAllText(path);
        string[] lines = content.Split(Environment.NewLine);

        if (!int.TryParse(lines[0], out int dimensions) && dimensions >= MinDimensions)
            problems.Add(Problem(1, $"The first line doesn't contain an integer for the dimensions of at least {MinDimensions}."));

        grid = new(Math.Max(dimensions, MinDimensions));

        Span<int> lineValues = stackalloc int[PartsPerLine];
        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;
            string line = lines[i];

            string[] parts = line.Split(',');
            if (parts.Length != PartsPerLine)
            {
                problems.Add(Problem(lineNumber, "Does not contain exactly 4 numbers for y, x, downSum, rightSum."));
                continue;
            }

            for (int j = 0; j < PartsPerLine; j++)
            {
                int partNumber = j + 1;

                if (!int.TryParse(parts[j], out var parsed))
                {
                    problems.Add(Problem(lineNumber, $"Part {partNumber} is not a valid integer."));
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

        return new GridConversionResult(problems);
    }

    private static string Problem(int line, string message) =>
        $"Line {line}: {message}";
}
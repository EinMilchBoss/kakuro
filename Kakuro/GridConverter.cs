using System.Windows.Markup;

namespace Kakuro;

public static class GridConverter
{
    private const int PartsPerLine = 4;
    private const int MinDimensions = 5;

    public static GridConversionResult ParseFromImport(string path, out Grid grid)
    {
        grid = default!;

        List<string> problems = [];

        string content = File.ReadAllText(path);
        string[] lines = content.Split(Environment.NewLine);

        if (!TryParseDimension(lines[0], problems, out int dimensions))
            return new GridConversionResult(problems);

        grid = new(dimensions);

        for (int i = 1; i < lines.Length; i++)
        {
            if (!TryParseLine(i + 1, lines[i], problems, out int[] lineValues))
                continue;

            int y = lineValues[0];
            int x = lineValues[1];
            grid[y, x] = new SetTile(x, y, lineValues[2], lineValues[3]);
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

    private static bool TryParseDimension(string line, List<string> problems, out int dimensions)
    {
        if (!int.TryParse(line, out dimensions) && dimensions >= MinDimensions)
        {
            problems.Add(Problem(1, $"The first line doesn't contain an integer for the dimensions of at least {MinDimensions}."));
            return false;
        }

        return true;
    }

    private static bool TryParseLine(int lineNumber, string line, List<string> problems, out int[] values)
    {
        values = new int[PartsPerLine];

        string[] parts = line.Split(',');
        if (parts.Length != PartsPerLine)
        {
            problems.Add(Problem(lineNumber, "Does not contain exactly 4 numbers for y, x, downSum, rightSum."));
            return false;
        }

        var allSuccessful = true;
        for (int i = 0; i < PartsPerLine; i++)
        {
            if (!int.TryParse(parts[i], out var value))
            {
                problems.Add(Problem(lineNumber, $"Part {i + 1} is not a valid integer."));
                allSuccessful = false;
                continue;
            }

            values[i] = value;
        }
        return allSuccessful;
    }

    private static string Problem(int line, string message) =>
        $"Line {line}: {message}";
}
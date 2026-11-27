namespace Kakuro;

public static class Program
{
    public static int Main()
    {
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
    }
}
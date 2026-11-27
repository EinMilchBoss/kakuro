namespace Kakuro;

public static class Program
{
    public static int Main()
    {
        var result = GridConverter.ParseFromImport("test.kkr", out var grid);
        if (!result.IsSuccess)
        {
            Console.Error.WriteLine("Error!");
            Console.Error.WriteLine(string.Join(Environment.NewLine, result.Problems));
            return 1;
        }

        PrintGrid(grid);

        return 0;
    }

    private static void PrintGrid(Grid grid)
    {
        for (int y = 0; y < grid.Dimensions; y++)
        {
            Console.Write('|');
            for (int x = 0; x < grid.Dimensions; x++)
            {
                Console.Write($"{grid[y, x].Print()}|");
            }
            Console.WriteLine();
        }
    }
}
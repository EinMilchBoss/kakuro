namespace Kakuro;

public class SetTile(int x, int y, int rightSum, int downSum) : Tile(x, y)
{
    public int RightSum { get; } = rightSum;
    public int DownSum { get; } = downSum;

    public bool IsEmpty { get; } = rightSum == 0 && downSum == 0;

    public IEnumerable<IEnumerable<int>> GetRightCombinations(int n) =>
        GetCombinations(RightSum, n);

    public IEnumerable<IEnumerable<int>> GetDownCombinations(int n) =>
        GetCombinations(DownSum, n);

    private static IEnumerable<int[]> GetCombinations(int sum, int n, int min = 1)
    {
        if (n == 1)
        {
            if (sum >= min)
                yield return new[] { sum };

            yield break;
        }

        for (int i = min; i <= sum / n; i++)
        {
            foreach (var rest in GetCombinations(sum - i, n - 1, i + 1))
            {
                yield return new[] { i }.Concat(rest).ToArray();
            }
        }
    }

    public override string ToString()
    {
        return $"{DownSum,2}\\{RightSum,-2}";
    }
}
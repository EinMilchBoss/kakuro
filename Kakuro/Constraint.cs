namespace Kakuro;

public class Constraint(int sum, IReadOnlyList<VarTile> varTiles)
{
    private readonly int _sum = sum;
    private readonly IReadOnlyList<VarTile> _varTiles = varTiles;

    public List<TileValuePossibilities> GetPossibilities()
    {
        static IEnumerable<List<int>> Recurse(int sum, int n, int min)
        {
            if (n == 1)
            {
                if (sum >= min)
                    yield return [sum];

                yield break;
            }

            for (int i = min; i <= sum / n; i++)
            {
                foreach (var rest in Recurse(sum - i, n - 1, i + 1))
                {
                    yield return new[] { i }.Concat(rest).ToList();
                }
            }
        }

        return Recurse(_sum, _varTiles.Count, 1)
            .Select(summands => new TileValuePossibilities(summands))
            .ToList();
    }
}
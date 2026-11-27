namespace Kakuro;

public class Constraint(int sum, IReadOnlyList<VarTile> varTiles)
{
    private readonly int _sum = sum;
    private readonly IReadOnlyList<VarTile> _varTiles = varTiles;
}
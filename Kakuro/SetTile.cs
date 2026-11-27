namespace Kakuro;

public class SetTile(int x, int y, int rightSum, int downSum) : Tile(x, y)
{
    public int RightSum { get; } = rightSum;
    public int DownSum { get; } = downSum;

    public bool IsEmpty { get; } = rightSum == 0 && downSum == 0;

    public override string Print()
    {
        return $"{DownSum,2}\\{RightSum,-2}";
    }
}
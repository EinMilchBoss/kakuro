namespace Kakuro;

public record struct TileValuePossibilities
{
    private const int MinValue = 1;
    private const int MaxValue = 9;

    private ushort _bits;

    public TileValuePossibilities(IEnumerable<int> values)
    {
        foreach (var value in values)
            AddValue(value);
    }

    public readonly IEnumerable<int> GetValues()
    {
        for (int value = MinValue; value <= MaxValue; value++)
        {
            if (ContainsValue(value))
                yield return value;
        }
    }

    public readonly bool ContainsValue(int value)
    {
        ThrowIfInvalidValue(value);

        var result = _bits & (1 << (value - 1));
        return result > 0;
    }

    public void AddValue(int value)
    {
        ThrowIfInvalidValue(value);

        _bits |= (ushort)(1 << (value - 1));
    }

    public void RemoveValue(int value)
    {
        ThrowIfInvalidValue(value);

        _bits &= (ushort)~(1 << (value - 1));
    }

    public void RetainIntersection(TileValuePossibilities other)
    {
        for (int value = MinValue; value <= MaxValue; value++)
        {
            if (!other.ContainsValue(value))
                RemoveValue(value);
        }
    }

    private static void ThrowIfInvalidValue(int value)
    {
        if (value is < MinValue or > MaxValue)
            throw new ArgumentOutOfRangeException(nameof(value));
    }
}
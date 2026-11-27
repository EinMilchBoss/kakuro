namespace Kakuro;

public class GridConversionResult(IReadOnlyCollection<string> problems)
{
    public bool IsSuccess => Problems.Count == 0;
    public IReadOnlyCollection<string> Problems { get; init; } = problems;
}
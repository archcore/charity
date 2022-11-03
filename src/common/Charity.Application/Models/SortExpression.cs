namespace Charity.Application.Models;

public class SortExpression
{
    public SortExpression(string? field, bool isDescending)
    {
        Field = field;
        IsDescending = isDescending;
    }

    public string? Field { get; }
    public bool IsDescending { get; }
}
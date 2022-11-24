namespace Charity.Api.Requests;

public class PaginatedListRequest
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Sort { get; set; }
    public string? Order { get; set; }
    public List<Guid>? Ids { get; set; }
}
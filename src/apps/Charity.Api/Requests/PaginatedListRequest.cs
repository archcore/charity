namespace Charity.Api.Requests;

public class PaginatedListRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public IDictionary<string, string>? Sort { get; set; }
}
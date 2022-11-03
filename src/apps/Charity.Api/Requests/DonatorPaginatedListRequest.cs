using Charity.Domain.Enums;

namespace Charity.Api.Requests;

public class DonatorPaginatedListRequest : PaginatedListRequest
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public DateTimeOffset? DateOfBirthMin { get; set; }
    public DateTimeOffset? DateOfBirthMax { get; set; }
    public string? Occupation { get; set; }
    public DonatorType? Type { get; set; }
}
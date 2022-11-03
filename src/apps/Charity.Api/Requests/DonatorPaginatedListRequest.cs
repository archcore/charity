using Charity.Domain.Enums;
using NodaTime;

namespace Charity.Api.Requests;

public class DonatorPaginatedListRequest : PaginatedListRequest
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public LocalDate? DateOfBirthMin { get; set; }
    public LocalDate? DateOfBirthMax { get; set; }
    public string? Occupation { get; set; }
    public DonatorType? Type { get; set; }
}
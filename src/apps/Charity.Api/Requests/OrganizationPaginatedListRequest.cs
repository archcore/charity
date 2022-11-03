using NodaTime;

namespace Charity.Api.Requests;

public class OrganizationPaginatedListRequest : PaginatedListRequest
{
    public string? LegalName { get; set; }
    public string? FriendlyName { get; set; }
    public string? Cause { get; set; }
    public string? Country { get; set; }
    public LocalDate? FoundationDateMin { get; set; }
    public LocalDate? FoundationDateMax { get; set; }
    public bool? IsAcceptingDonations { get; set; }
}
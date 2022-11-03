namespace Charity.Api.Requests;

public class DonationPaginatedListRequest : PaginatedListRequest
{
    public Guid? OrganizationId { get; set; }
    public Guid? DonatorId { get; set; }
    public decimal? ValueMin { get; set; }
    public decimal? ValueMax { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DonatedAtMin { get; set; }
    public DateTimeOffset? DonatedAtMax { get; set; }
}
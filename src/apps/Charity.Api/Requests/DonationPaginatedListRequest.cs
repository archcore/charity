using NodaTime;

namespace Charity.Api.Requests;

public class DonationPaginatedListRequest : PaginatedListRequest
{
    public Guid? OrganizationId { get; set; }
    public Guid? DonatorId { get; set; }
    public decimal? ValueMin { get; set; }
    public decimal? ValueMax { get; set; }
    public string? Description { get; set; }
    public LocalDateTime? DonatedAtMin { get; set; }
    public LocalDateTime? DonatedAtMax { get; set; }
}
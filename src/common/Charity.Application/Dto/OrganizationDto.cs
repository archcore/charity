using Charity.Application.Common.Dto;
using NodaTime;

namespace Charity.Application.Dto;

public class OrganizationDto : BaseDto
{
    public string? LegalName { get; set; }
    public string? FriendlyName { get; set; }
    public string? Cause { get; set; }
    public string? Description { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public LocalDate FoundationDate { get; set; }
    public bool IsAcceptingDonations { get; set; }
    
    public virtual List<DonationDto>? Donations { get; set; }
}
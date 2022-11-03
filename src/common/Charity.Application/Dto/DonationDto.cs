using Charity.Application.Common.Dto;
using NodaTime;

namespace Charity.Application.Dto;

public class DonationDto : BaseDto
{
    public Guid OrganizationId { get; set; }
    public Guid DonatorId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public LocalDateTime DonatedAt { get; set; }
    
    public virtual DonatorDto? Donator { get; set; }
    public virtual OrganizationDto? Organization { get; set; }
}
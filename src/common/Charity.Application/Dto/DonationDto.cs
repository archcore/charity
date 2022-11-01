namespace Charity.Application.Dto;

public class DonationDto
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid DonatorId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset DonatedAt { get; set; }
    
    public virtual DonatorDto? Donator { get; set; }
    public virtual OrganizationDto? Organization { get; set; }
}
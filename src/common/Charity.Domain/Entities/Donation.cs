using Charity.Domain.Common;
using NodaTime;

namespace Charity.Domain.Entities;

public class Donation : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid DonatorId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public LocalDateTime DonatedAt { get; set; }
    
    public virtual Donator? Donator { get; set; }
    public virtual Organization? Organization { get; set; }
}
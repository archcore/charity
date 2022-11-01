using Charity.Domain.Common;

namespace Charity.Domain.Entities;

public class Donation : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid DonatorId { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DonatedAt { get; set; }
    
    public virtual Donator Donator { get; set; }
    public virtual Organization Organization { get; set; }
}
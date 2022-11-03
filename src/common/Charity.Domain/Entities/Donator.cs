using Charity.Domain.Common;
using Charity.Domain.Enums;
using NodaTime;

namespace Charity.Domain.Entities;

public class Donator : BaseEntity
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public LocalDate? DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public DonatorType Type { get; set; }
    
    public virtual List<Donation>? Donations { get; set; }
}
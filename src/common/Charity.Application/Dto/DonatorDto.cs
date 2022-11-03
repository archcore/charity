using Charity.Application.Common.Dto;
using Charity.Domain.Enums;

namespace Charity.Application.Dto;

public class DonatorDto : BaseDto
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public DonatorType Type { get; set; }
    
    public virtual List<DonationDto>? Donations { get; set; }
}
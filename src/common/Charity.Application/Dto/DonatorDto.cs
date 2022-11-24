using Charity.Application.Common.Dto;
using Charity.Domain.Enums;
using NodaTime;

namespace Charity.Application.Dto;

public record DonatorDto : BaseDto
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public LocalDate? DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public DonatorType Type { get; set; }
}
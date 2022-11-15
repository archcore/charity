namespace Charity.Application.Common.Dto;

public abstract record BaseDto
{
    public Guid Id { get; set; }
}
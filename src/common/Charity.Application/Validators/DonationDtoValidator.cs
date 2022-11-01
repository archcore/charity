using Charity.Application.Dto;
using FluentValidation;

namespace Charity.Application.Validators;

public class DonationDtoValidator : AbstractValidator<DonationDto>
{
    public DonationDtoValidator()
    {
        RuleFor(m => m.Description)
            .NotEmpty();

        RuleFor(m => m.Value)
            .GreaterThan(0);

        RuleFor(m => m.DonatedAt)
            .LessThan(DateTimeOffset.Now);
    }
}
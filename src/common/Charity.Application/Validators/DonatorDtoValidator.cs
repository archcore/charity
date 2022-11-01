using Charity.Application.Dto;
using Charity.Domain.Enums;
using FluentValidation;

namespace Charity.Application.Validators;

public class DonatorDtoValidator : AbstractValidator<DonatorDto>
{
    public DonatorDtoValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        RuleFor(m => m.Document)
            .NotEmpty();

        When(d => d.Type == DonatorType.Individual, () =>
        {
            RuleFor(d => d.Occupation)
                .NotEmpty();
        });
    }
}
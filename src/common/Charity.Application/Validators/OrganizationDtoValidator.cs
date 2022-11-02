using Charity.Application.Dto;
using FluentValidation;

namespace Charity.Application.Validators;

public class OrganizationDtoValidator : AbstractValidator<OrganizationDto>
{
    public OrganizationDtoValidator()
    {
        RuleFor(m => m.LegalName)
            .NotEmpty();
        
        RuleFor(m => m.Cause)
            .NotEmpty();
        
        RuleFor(m => m.Country)
            .NotEmpty()
            .Length(2);
    }
}
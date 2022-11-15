using Charity.Application.Dto;
using Charity.Application.Validators;
using FluentAssertions;
using FluentValidation.Results;
using NodaTime;

namespace Charity.Tests.Unit.Application.Validators;

public class DonationDtoValidatorTests
{
    private readonly DonationDtoValidator _validator;

    public DonationDtoValidatorTests()
    {
        _validator = new DonationDtoValidator();
    }
    
    [Fact]
    public void Validate_WhenDataIsValid_ReturnsTrue()
    {
        // Arrange
        var dto = new DonationDto
        {
            Description = "My first donation",
            DonatedAt = LocalDateTime.FromDateTime(DateTime.Today),
            Value = 5555.55m
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WhenDataIsInvalid_ReturnsFalse()
    {
        // Arrange
        var dto = new DonationDto
        {
            Description = "",
            DonatedAt = LocalDateTime.FromDateTime(DateTime.Now.AddDays(1)),
            Value = -1
        };
        
        // Act
        var result = _validator.Validate(dto);
        
        // Assert NotEmptyValidator GreaterThanValidator LessThanValidator
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationFailure
                {
                    ErrorCode = "NotEmptyValidator",
                    PropertyName = "Description"
                },
                new ValidationFailure
                {
                    ErrorCode = "GreaterThanValidator",
                    PropertyName = "Value"
                },
                new ValidationFailure
                {
                    ErrorCode = "LessThanValidator",
                    PropertyName = "DonatedAt"
                }
            },
            opt => opt
                .Including(m => m.ErrorCode)
                .Including(m => m.PropertyName));
    }
}
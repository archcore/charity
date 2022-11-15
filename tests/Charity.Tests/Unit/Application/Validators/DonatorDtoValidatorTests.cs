using Charity.Application.Dto;
using Charity.Application.Validators;
using Charity.Domain.Enums;
using FluentAssertions;
using FluentValidation.Results;
using NodaTime;

namespace Charity.Tests.Unit.Application.Validators;

public class DonatorDtoValidatorTests
{
    private static readonly DonatorDto ValidIndividual = new()
    {
        Name = "Rich Donator Guy",
        Type = DonatorType.Individual,
        Document = "11122233344",
        Occupation = "Blacksmith",
        DateOfBirth = new LocalDate(1997, 01, 10)
    };
    private static readonly DonatorDto ValidCompany = new()
    {
        Name = "Acme Corporation",
        Type = DonatorType.Company,
        Document = "J123456",
        Occupation = "Sports Coaching",
        DateOfBirth = new LocalDate(2020, 01, 01)
    };
    private static readonly DonatorDto ValidNonProfit = new()
    {
        Name = "Legalong",
        Type = DonatorType.NonProfit,
        Document = "ASDF987",
        DateOfBirth = new LocalDate(2020, 12, 12)
    };

    public static readonly IEnumerable<object[]> AllDonators = new[] {
        new object[] { ValidIndividual },
        new object[] { ValidCompany },
        new object[] { ValidNonProfit }
    };
    
    private readonly DonatorDtoValidator _validator;

    public DonatorDtoValidatorTests()
    {
        _validator = new DonatorDtoValidator();
    }
    
    [Theory]
    [MemberData(nameof(AllDonators))]
    public void Validate_WhenDataIsValid_ReturnsTrue(DonatorDto donator)
    {
        // Act
        var result = _validator.Validate(donator);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WhenIndividualOccupationIsEmpty_ReturnsFalse()
    {
        // Arrange
        var donator = ValidIndividual with
        {
            Occupation = ""
        };
        
        // Act
        var result = _validator.Validate(donator);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationFailure
                {
                    ErrorCode = "NotEmptyValidator",
                    PropertyName = "Occupation"
                }
            },
            opt => opt
                .Including(m => m.ErrorCode)
                .Including(m => m.PropertyName));
    }

    [Theory]
    [MemberData(nameof(AllDonators))]
    public void Validate_WhenDataIsInvalid_ReturnsFalse(DonatorDto donator)
    {
        // Arrange
        donator = donator with
        {
            Name = "",
            Document = "",
            Occupation = ""
        };
        
        // Act
        var result = _validator.Validate(donator);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[]
            {
                new ValidationFailure
                {
                    ErrorCode = "NotEmptyValidator",
                    PropertyName = "Name"
                },
                new ValidationFailure
                {
                    ErrorCode = "NotEmptyValidator",
                    PropertyName = "Document"
                }
            }.Concat(donator.Type is DonatorType.Individual
                ? new[]
                {
                    new ValidationFailure
                    {
                        ErrorCode = "NotEmptyValidator",
                        PropertyName = "Occupation"
                    }
                }
                : Array.Empty<ValidationFailure>()
            ),
            opt => opt
                .Including(m => m.ErrorCode)
                .Including(m => m.PropertyName));
    }
}
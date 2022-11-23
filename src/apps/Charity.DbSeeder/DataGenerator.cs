using Bogus;
using Charity.Domain.Entities;
using Charity.Domain.Enums;
using NodaTime;

namespace Charity.DbSeeder;

public static class DataGenerator
{
    public record Result
    {
        public List<Organization> Organizations { get; } = new();
        public List<Donator> Donators { get; } = new();
        public List<Donation> Donations { get; } = new();
    }

    private static readonly Faker<Organization> OrganizationFaker = new Faker<Organization>()
        .RuleFor(m => m.Id, _ => Guid.NewGuid())
        .RuleFor(m => m.LegalName, f => f.Company.CompanyName())
        .RuleFor(m => m.FriendlyName, (_, o) => $"{o.LegalName} Friendly")
        .RuleFor(m => m.Cause, f => f.Company.CatchPhrase())
        .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
        .RuleFor(m => m.Street, f => f.Address.StreetAddress())
        .RuleFor(m => m.City, f => f.Address.City())
        .RuleFor(m => m.State, f => f.Address.State())
        .RuleFor(m => m.ZipCode, f => f.Address.ZipCode())
        .RuleFor(m => m.Country, f => f.Address.CountryCode())
        .RuleFor(m => m.FoundationDate, f => LocalDate.FromDateTime(f.Date.Past(30)))
        .RuleFor(m => m.IsAcceptingDonations, f => f.Random.Bool());
    
    private static readonly Faker<Donator> DonatorFaker = new Faker<Donator>()
        .RuleFor(m => m.Id, _ => Guid.NewGuid())
        .RuleFor(m => m.Type, f => f.Random.Enum<DonatorType>())
        .RuleFor(m => m.Name, (f, d) => d.Type == DonatorType.Individual ? f.Name.FullName() : f.Company.CompanyName())
        .RuleFor(m => m.Document, (f, d) => d.Type == DonatorType.Individual ? f.Random.AlphaNumeric(9) : f.Random.AlphaNumeric(14))
        .RuleFor(m => m.DateOfBirth, f => LocalDate.FromDateTime(f.Date.Past(70)))
        .RuleFor(m => m.Occupation, (f, d) => d.Type == DonatorType.Individual ? f.Name.JobTitle() : f.Name.JobArea());

    private static Faker<Donation> GetDonationFaker(List<Organization> organizations, List<Donator> donators) =>
        new Faker<Donation>()
            .RuleFor(m => m.Id, _ => Guid.NewGuid())
            .RuleFor(m => m.OrganizationId, f => f.PickRandom(organizations).Id)
            .RuleFor(m => m.DonatorId, f => f.PickRandom(donators).Id)
            .RuleFor(m => m.Value, f => f.Random.Number(100, 5000000))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.DonatedAt, f => LocalDateTime.FromDateTime(f.Date.Past(5)));

    public static Result GenerateData()
    {
        var result = new Result();
            
        result.Organizations.AddRange(OrganizationFaker.Generate(100));
        result.Donators.AddRange(DonatorFaker.Generate(600));
        result.Donations.AddRange(GetDonationFaker(result.Organizations, result.Donators).Generate(3600));

        return result;
    }
}
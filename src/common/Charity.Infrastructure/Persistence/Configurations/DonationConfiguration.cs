using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Infrastructure.Persistence.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.Property(m => m.Description)
            .IsRequired();

        builder.HasOne(m => m.Donator)
            .WithMany(m => m.Donations)
            .HasForeignKey(m => m.DonatorId);
        
        builder.HasOne(m => m.Organization)
            .WithMany(m => m.Donations)
            .HasForeignKey(m => m.OrganizationId);
    }
}
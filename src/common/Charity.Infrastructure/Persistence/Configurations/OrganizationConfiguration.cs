using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Infrastructure.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(m => m.LegalName)
            .IsRequired();
        
        builder.Property(m => m.Cause)
            .IsRequired();
        
        builder.Property(m => m.Country)
            .IsRequired()
            .HasMaxLength(2);
    }
}
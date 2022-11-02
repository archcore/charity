using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Infrastructure.Persistence.Configurations;

public class DonatorConfiguration : IEntityTypeConfiguration<Donator>
{
    public void Configure(EntityTypeBuilder<Donator> builder)
    {
        builder.Property(m => m.Name)
            .IsRequired();
        
        builder.Property(m => m.Document)
            .IsRequired();
    }
}
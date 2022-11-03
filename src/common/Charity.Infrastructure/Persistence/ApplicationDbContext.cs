using System.Reflection;
using Charity.Domain.Common;
using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Charity.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly Instant _now = SystemClock.Instance.GetCurrentInstant();
    
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<Organization>? Organizations { get; set; }
    public DbSet<Donator>? Donators { get; set; }
    public DbSet<Donation>? Donations { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
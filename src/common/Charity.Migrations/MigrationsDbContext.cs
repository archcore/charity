using Charity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Charity.Migrations;

public sealed class MigrationsDbContext : ApplicationDbContext
{
    public MigrationsDbContext(DbContextOptions options) : base(options)
    {
    }
}
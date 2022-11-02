using Charity.Application.Interfaces;
using Charity.Infrastructure.Persistence;
using Charity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charity.Infrastructure;

public static class DependencyInjection
{
    private const string MigrationsAssembly = "Charity.Migrations";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services
        .AddServiceImplementations()
        .AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("ApplicationDbContext"),
                b => b.MigrationsAssembly(MigrationsAssembly))
        );

    private static IServiceCollection AddServiceImplementations(this IServiceCollection services) => services
        .AddScoped<IDonationService, DonationService>()
        .AddScoped<IDonatorService, DonatorService>()
        .AddScoped<IOrganizationService, OrganizationService>();
}
using Charity.Application.Common.Interfaces;
using Charity.Application.Interfaces;
using Charity.Infrastructure.Common.Adapters;
using Charity.Infrastructure.Persistence;
using Charity.Infrastructure.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charity.Infrastructure;

public static class DependencyInjection
{
    private const string MigrationsAssembly = "Charity.Migrations";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services
        .AddServiceImplementations()
        .AddAdapters()
        .AddMapster()
        .AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("ApplicationDbContext"),
                b => b.MigrationsAssembly(MigrationsAssembly))
        );

    private static IServiceCollection AddServiceImplementations(this IServiceCollection services) => services
        .AddScoped<IDonationService, DonationService>()
        .AddScoped<IDonatorService, DonatorService>()
        .AddScoped<IOrganizationService, OrganizationService>();
    
    private static IServiceCollection AddAdapters(this IServiceCollection services) => services
        .AddScoped<IFilterAdapter, FilterAdapter>()
        .AddScoped<ISortAdapter, SortAdapter>();

    private static IServiceCollection AddMapster(this IServiceCollection services) => services
        .AddSingleton(TypeAdapterConfig.GlobalSettings)
        .AddScoped<IMapper, ServiceMapper>();
}
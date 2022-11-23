using Charity.DbSeeder;
using Charity.Infrastructure;
using Charity.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddInfrastructure(configuration);

using var scope = services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

Console.WriteLine("DbContext resolved");

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();
Console.WriteLine("Database created");

var data = DataGenerator.GenerateData();
dbContext.Organizations!.AddRange(data.Organizations);
Console.WriteLine("Added organizations");
dbContext.Donators!.AddRange(data.Donators);
Console.WriteLine("Added donators");
dbContext.Donations!.AddRange(data.Donations);
Console.WriteLine("Added donations");

Console.WriteLine("Saving changes...");
dbContext.SaveChanges();
Console.WriteLine("All done!");
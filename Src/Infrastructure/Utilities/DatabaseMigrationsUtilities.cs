using Infrastructure.SeedDatas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utilities;

public sealed class DatabaseMigrationsUtilities
{
    public static async Task RunDatabaseMigrationsAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        applicationDbContext.Database.EnsureDeleted();
        applicationDbContext.Database.EnsureCreated();

        await SeedData.SeedRequiredDataAsync(applicationDbContext);
    }
}
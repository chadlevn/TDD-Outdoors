using Infrastructure.SeedDatas.Seeds;

namespace Infrastructure.SeedDatas;

public static class SeedData
{
    public static async Task SeedRequiredDataAsync(ApplicationDbContext applicationDbContext)
    {
        await ActivitiesSeed.SeedActivitiesIfEmptyAsync(applicationDbContext);

        await applicationDbContext.SaveChangesAsync();
    }
}
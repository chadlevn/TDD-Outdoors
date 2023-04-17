using Domain.Constants;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedDatas.Seeds;

internal sealed class ActivitiesSeed
{
    private static List<Activity> ActivitySeeds => new List<Activity>()
    {
        new Activity()
        {
            Name = "First Activity",
            Type = ActivityTypeConstants.Hiking,
            Date = DateTime.Now
        }
    };

    public static async Task SeedActivitiesIfEmptyAsync(ApplicationDbContext applicationDbContext)
    {
        if (!await applicationDbContext.Activities.AnyAsync())
        {
            applicationDbContext.Activities.AddRange(ActivitySeeds);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
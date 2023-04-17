using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests;

internal static class ApplicationDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString(),
            o => o.EnableNullChecks(false))
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        context.SaveChanges();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Activity> Activities { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void Dispose();
}
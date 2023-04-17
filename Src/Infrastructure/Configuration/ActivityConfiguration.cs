using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable(nameof(Activity), "dbo");

        builder.HasKey(b => b.ActivityId);

        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Type).IsRequired();
        builder.Property(b => b.Description);
        builder.Property(b => b.Date).IsRequired();
    }
}
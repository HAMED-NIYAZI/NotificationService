using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;

namespace Notification.Persistence.Configurations;

public sealed class NotificationApplicationConfiguration
    : IEntityTypeConfiguration<NotificationApplication>
{
    public void Configure(
        EntityTypeBuilder<NotificationApplication> builder)
    {
        builder.ToTable(
            "NotificationApplications",
            "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ApiKey)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasPrecision(3)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasPrecision(3);

        builder.HasIndex(x => x.ApiKey)
            .IsUnique();
    }
}
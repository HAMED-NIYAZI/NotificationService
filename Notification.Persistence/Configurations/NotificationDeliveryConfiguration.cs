using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;
using Notification.Domain.Entities.Enums;

namespace Notification.Persistence.Configurations;

public sealed class NotificationDeliveryConfiguration
    : IEntityTypeConfiguration<NotificationDelivery>
{
    public void Configure(
        EntityTypeBuilder<NotificationDelivery> builder)
    {
        builder.ToTable(
            "NotificationDeliveries",
            "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.NotificationId)
            .IsRequired();

        builder.Property(x => x.ClientConnectionId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<byte>()
            .IsRequired();

        builder.Property(x => x.RetryCount)
            .IsRequired();

        builder.Property(x => x.LeaseId);

        builder.Property(x => x.LeaseUntil)
            .HasPrecision(3);

        builder.Property(x => x.SentAt)
            .HasPrecision(3);
        builder.Property(x => x.NextRetryAt)
    .HasPrecision(3);

        builder.Property(x => x.DeliveredAt)
            .HasPrecision(3);

        builder.Property(x => x.ReadAt)
            .HasPrecision(3);

        builder.Property(x => x.FailedAt)
            .HasPrecision(3);

        builder.Property(x => x.LastError)
            .HasMaxLength(4000);

        builder.Property(x => x.CreatedAt)
            .HasPrecision(3)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasPrecision(3)
            .IsRequired();

        builder.HasOne(x => x.Notification)
            .WithMany(x => x.Deliveries)
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ClientConnection)
            .WithMany(x => x.Deliveries)
            .HasForeignKey(x => x.ClientConnectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.NotificationId);

        builder.HasIndex(x => x.ClientConnectionId);

        builder.HasIndex(x => new
        {
            x.Status,
            x.CreatedAt
        });
    }
}
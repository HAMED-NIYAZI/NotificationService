using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;

namespace Notification.Persistence.Configurations;

public sealed class NotificationConfiguration
    : IEntityTypeConfiguration<Notification.Domain.Entities.Notification>
{
    public void Configure(
        EntityTypeBuilder<Notification.Domain.Entities.Notification> builder)
    {
        builder.ToTable(
            "Notifications",
            "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.ApplicationId)
            .IsRequired();

        builder.Property(x => x.RecipientId)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Message)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasPrecision(3)
            .IsRequired();

        builder.HasOne(x => x.Application)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ApplicationId,
            x.RecipientId,
            x.CreatedAt
        });
    }
}
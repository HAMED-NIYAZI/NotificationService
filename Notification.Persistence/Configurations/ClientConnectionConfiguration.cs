using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;

namespace Notification.Persistence.Configurations;

public sealed class ClientConnectionConfiguration
    : IEntityTypeConfiguration<ClientConnection>
{
    public void Configure(
        EntityTypeBuilder<ClientConnection> builder)
    {
        builder.ToTable(
            "ClientConnections",
            "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.ApplicationId)
            .IsRequired();

        builder.Property(x => x.RecipientId)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ConnectionId)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.ConnectedAt)
            .HasPrecision(3)
            .IsRequired();

        builder.Property(x => x.DisconnectedAt)
            .HasPrecision(3);

        builder.HasOne(x => x.Application)
            .WithMany(x => x.ClientConnections)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ConnectionId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.ApplicationId,
            x.RecipientId,
            x.IsActive
        });
    }
}
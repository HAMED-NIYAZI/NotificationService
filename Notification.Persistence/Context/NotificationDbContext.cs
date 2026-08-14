using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entities;

namespace Notification.Persistence.Context;

 

public sealed class NotificationDbContext
    : DbContext
{
    public NotificationDbContext(
        DbContextOptions<NotificationDbContext> options)
        : base(options)
    {
    }

    public DbSet<NotificationApplication>
        NotificationApplications =>
        Set<NotificationApplication>();

    public DbSet<Notification.Domain.Entities.Notification>
        Notifications =>
        Set<Notification.Domain.Entities.Notification>();

    public DbSet<ClientConnection>
        ClientConnections =>
        Set<ClientConnection>();

    public DbSet<NotificationDelivery>
        NotificationDeliveries =>
        Set<NotificationDelivery>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(NotificationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain.Entities.Enums;
using Notification.Persistence.Context;

namespace Notification.Persistence.Repositories;

internal class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationRepository(
        NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Domain.Entities.Notification notification)
    {
        await _dbContext.Notifications.AddAsync(notification);
    }

    public Task<Domain.Entities.Notification> GetAsync(Guid applicationId, Guid notificationId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Domain.Entities.Notification>> GetDeliveredAsync(Guid applicationId, string recipientId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Domain.Entities.Notification>> GetReadAsync(Guid applicationId, string recipientId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Domain.Entities.Notification>> GetUndeliveredAsync(Guid applicationId, string recipientId)
    {
        return await _dbContext.Notifications
            .Where(x =>
                x.ApplicationId == applicationId &&
                x.RecipientId == recipientId &&
                x.Deliveries.All(
                    d =>
                        d.Status !=
                            NotificationDeliveryStatus.Delivered &&
                        d.Status !=
                            NotificationDeliveryStatus.Read))
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

 

    public Task<IReadOnlyList<Domain.Entities.Notification>> GetUnReadAsync(Guid applicationId, string recipientId)
    {
        throw new NotImplementedException();
    }
}

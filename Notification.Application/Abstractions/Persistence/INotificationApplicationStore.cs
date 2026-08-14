using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Persistence;

public interface INotificationApplicationRepository
{
    Task<NotificationApplication?> GetByApiKeyAsync(
        string ApiKey
        );
}
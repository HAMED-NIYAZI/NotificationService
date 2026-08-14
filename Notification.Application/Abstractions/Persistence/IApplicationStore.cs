using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Persistence;

public interface IApplicationRepository
{
    Task<NotificationApplication?> GetByApiKeyAsync(
        Guid? apiKey
        );
}
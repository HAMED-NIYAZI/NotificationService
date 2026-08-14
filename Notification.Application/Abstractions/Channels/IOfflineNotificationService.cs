namespace Notification.Application.Abstractions.Channels;

public interface IOfflineNotificationService
{
    Task SyncAsync(
        Guid applicationId,
        string recipientId,
        Guid connectionId
        );
}
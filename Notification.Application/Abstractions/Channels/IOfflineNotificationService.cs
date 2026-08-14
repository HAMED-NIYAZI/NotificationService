namespace Notification.Application.Abstractions.Channels;

public interface IOfflineNotificationService
{
    Task SyncAsync(
        string applicationId,
        string recipientId,
        Guid connectionId
        );
}
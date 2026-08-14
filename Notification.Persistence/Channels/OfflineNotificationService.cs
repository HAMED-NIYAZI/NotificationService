using Notification.Application.Abstractions.Channels;
using Notification.Application.Abstractions.Persistence;

namespace Notification.Persistence.Channels;

public sealed class OfflineNotificationService
    : IOfflineNotificationService
{
    private readonly INotificationDeliveryRepository _deliveryRepository;
    private readonly INotificationChannel _channel;

    public OfflineNotificationService(
        INotificationDeliveryRepository deliveryRepository,
        INotificationChannel channel)
    {
        _deliveryRepository = deliveryRepository;
        _channel = channel;
    }

    public async Task SyncAsync(
        Guid applicationId,
        string recipientId,
        string connectionId
        )
    {
        var deliveries =
            await _deliveryRepository
                .GetPendingForRecipientAsync(
                    applicationId,
                    recipientId
                    );

        foreach (var delivery in deliveries)
        {
            var message =
                new NotificationDeliveryMessage(
                    delivery.Id,
                    delivery.NotificationId,
                    connectionId,
                    delivery.Notification.Type,
                    delivery.Notification.Title,
                    delivery.Notification.Message,
                    delivery.Notification.CreatedAt);

            await _channel.SendAsync(
                message
                );
        }
    }

    public Task SyncAsync(Guid applicationId, string recipientId, Guid connectionId)
    {
        throw new NotImplementedException();
    }
}
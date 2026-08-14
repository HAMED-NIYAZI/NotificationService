using Notification.Application.Abstractions.Channels;
using Notification.Application.Abstractions.Persistence;

namespace Notification.Application.Features.Deliveries.Send;

public sealed class NotificationDeliverySender
{
    private readonly INotificationChannel _channel;
    private readonly INotificationDeliveryRepository _deliveryRepository;

    public NotificationDeliverySender(
        INotificationChannel channel,
        INotificationDeliveryRepository deliveryRepository)
    {
        _channel = channel;
        _deliveryRepository = deliveryRepository;
    }

    public async Task SendAsync(
        NotificationDeliveryMessage message, Guid leaseId
        )
    {
        try
        {
            await _channel.SendAsync(
                message
                );

            await _deliveryRepository.MarkAsSentAsync(
                message.DeliveryId, leaseId

                );
        }
        catch (Exception ex)
        {
            await _deliveryRepository.MarkAsFailedAsync(
                message.DeliveryId, leaseId,

                ex.Message
                );

            throw;
        }
    }
}
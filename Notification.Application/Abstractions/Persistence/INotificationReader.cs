using Notification.Application.Abstractions.Channels;

namespace Notification.Application.Abstractions.Persistence
{
    public interface INotificationReader
    {
        Task<NotificationDeliveryMessage?>
            GetDeliveryMessageAsync(
                Guid deliveryId
                );
    }
}


 

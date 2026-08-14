using Notification.Application.Abstractions.Channels;

public interface INotificationChannel
{
    Task SendAsync(
        NotificationDeliveryMessage message
        );
}
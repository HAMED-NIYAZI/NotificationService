namespace Notification.Application.Abstractions.Queue
{
 
    public interface INotificationQueue
    {
        Task<IReadOnlyList<NotificationDeliveryWorkItem>>
            ClaimAsync(
                int batchSize
                );
    }

}
 
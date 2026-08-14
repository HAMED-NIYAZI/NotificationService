namespace Notification.Application.Features.Deliveries.ProcessNotificationDelivery
{
    public interface INotificationDeliveryProcessor
    {
        Task ProcessBatchAsync(
            CancellationToken cancellationToken);
    }


     }

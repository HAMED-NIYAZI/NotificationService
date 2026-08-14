using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Persistence;

public interface INotificationDeliveryRepository
{
    Task AddRangeAsync(
        IReadOnlyCollection<NotificationDelivery> deliveries
        );

    Task<NotificationDelivery?> ClaimNextAsync(
        Guid leaseId,
        TimeSpan leaseDuration
        );

    Task<bool> MarkAsSentAsync(
        Guid deliveryId,
        Guid leaseId
        );

    Task<bool> MarkAsFailedAsync(
        Guid deliveryId,
        Guid leaseId,
        string error
        );

    Task<NotificationDelivery?> GetForAcknowledgementAsync(
        Guid deliveryId,
        string applicationId,
        string recipientId,
        string connectionId
        );

    Task<bool> MarkAsDeliveredAsync(
        Guid deliveryId
        );

    Task<bool> MarkAsReadAsync(
        Guid deliveryId
        );

    Task<IReadOnlyList<NotificationDelivery>>
    GetPendingForRecipientAsync(
        string applicationId,
        string recipientId
        );

}
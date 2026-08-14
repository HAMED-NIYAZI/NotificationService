using Notification.Domain.Entities.Enums;
using Notification.Domain.Exceptions;

namespace Notification.Domain.Entities;

 

public sealed class NotificationDelivery
{
    private NotificationDelivery()
    {
    }

    private NotificationDelivery(
        Guid id,
        Guid notificationId,
        Guid clientConnectionId)
    {
        if (id == Guid.Empty)
            throw new NotificationDomainException(
                "Delivery id cannot be empty.");

        if (notificationId == Guid.Empty)
            throw new NotificationDomainException(
                "Notification id cannot be empty.");

        if (clientConnectionId == Guid.Empty)
            throw new NotificationDomainException(
                "Client connection id cannot be empty.");

        Id = id;
        NotificationId = notificationId;
        ClientConnectionId = clientConnectionId;

        Status = NotificationDeliveryStatus.Pending;

        RetryCount = 0;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Guid Id { get; private set; }

    public Guid NotificationId { get; private set; }

    public Guid ClientConnectionId { get; private set; }

    public NotificationDeliveryStatus Status { get; private set; }

    public int RetryCount { get; private set; }

    public Guid? LeaseId { get; private set; }

    public DateTime? LeaseUntil { get; private set; }
    public DateTime? NextRetryAt { get; private set; }
    public DateTime? SentAt { get; private set; }

    public DateTime? DeliveredAt { get; private set; }

    public DateTime? ReadAt { get; private set; }

    public DateTime? FailedAt { get; private set; }

    public string? LastError { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public Notification Notification { get; private set; } = null!;

    public ClientConnection ClientConnection { get; private set; } = null!;

    public static NotificationDelivery Create(
        Guid notificationId,
        Guid clientConnectionId)
    {
        return new NotificationDelivery(
            Guid.NewGuid(),
            notificationId,
            clientConnectionId);
    }
}
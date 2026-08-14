using Notification.Domain.Entities.Enums;

namespace Notification.Domain.Entities;

public class NotificationAudit
{
    private NotificationAudit()
    {
    }

    private NotificationAudit(
        Guid id,
        Guid notificationId,
        Guid? deliveryId,
        NotificationAuditAction action,
        DateTime occurredAt,
        string? metadata)
    {
        Id = id;
        NotificationId = notificationId;
        DeliveryId = deliveryId;
        Action = action;
        OccurredAt = occurredAt;
        Metadata = metadata;
    }

    public Guid Id { get; private set; }

    public Guid NotificationId { get; private set; }

    public Guid? DeliveryId { get; private set; }

    public NotificationAuditAction Action { get; private set; }

    public DateTime OccurredAt { get; private set; }

    public string? Metadata { get; private set; }

    public Notification Notification { get; private set; } = null!;

    public NotificationDelivery? Delivery { get; private set; }

    public static NotificationAudit Create(
        Guid notificationId,
        NotificationAuditAction action,
        Guid? deliveryId = null,
        string? metadata = null)
    {
        return new NotificationAudit(
            Guid.NewGuid(),
            notificationId,
            deliveryId,
            action,
            DateTime.UtcNow,
            metadata);
    }
}
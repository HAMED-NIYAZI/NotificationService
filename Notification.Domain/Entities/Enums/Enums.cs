namespace Notification.Domain.Entities.Enums;

public enum NotificationStatus : byte
{
    Created = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Expired = 5
}

 
public enum NotificationDeliveryStatus : byte
{
    Pending = 1,

    Processing = 2,

    Sent = 3,

    Delivered = 4,

    Read = 5,

    Failed = 6
}

public enum NotificationChannel : byte
{
    SignalR = 1,
    WebPush = 2
}

public enum NotificationAuditAction : byte
{
    NotificationCreated = 1,

    RecipientCreated = 2,

    DeliveryCreated = 3,

    DeliveryClaimed = 4,

    DeliverySent = 5,

    DeliveryAcknowledged = 6,

    NotificationRead = 7,

    DeliveryFailed = 8,

    RetryScheduled = 9,

    DeadLettered = 10,

    Expired = 11
}


 
public enum NotificationType : byte
{
    General = 1,
    Info = 2,
    Success = 3,
    Warning = 4,
    Error = 5
}
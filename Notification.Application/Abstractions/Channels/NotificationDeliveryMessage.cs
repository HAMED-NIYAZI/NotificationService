namespace Notification.Application.Abstractions.Channels;

public sealed record NotificationDeliveryMessage(
    Guid DeliveryId,
    Guid NotificationId,
    string ConnectionId,
    string Type,
    string Title,
    string Message,
    DateTime CreatedAt);
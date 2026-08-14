namespace Notification.Application.Features.Notifications.Queries.GetUndeliveredNotification;

public class GetUndeliveredNotificationResult
{
     public Guid NotificationId { get; set; }

    public string? RecipientId { get; set; }

    public string? Type { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }
    public DateTime? ExpireAt { get; set; }

}

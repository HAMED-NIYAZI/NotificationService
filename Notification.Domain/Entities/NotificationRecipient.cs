namespace Notification.Domain.Entities;

public class NotificationRecipient
{
    private NotificationRecipient()
    {
    }

    private NotificationRecipient(
        Guid id,
        Guid notificationId,
        string recipientId,
        DateTime createdAt)
    {
        Id = id;
        NotificationId = notificationId;
        RecipientId = recipientId;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public Guid NotificationId { get; private set; }

    public string RecipientId { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public Notification Notification { get; private set; } = null!;

    public static NotificationRecipient Create(
        Guid notificationId,
        string recipientId)
    {
        if (notificationId == Guid.Empty)
            throw new ArgumentException(
                "NotificationId is required.");

        if (string.IsNullOrWhiteSpace(recipientId))
            throw new ArgumentException(
                "RecipientId is required.");

        return new NotificationRecipient(
            Guid.NewGuid(),
            notificationId,
            recipientId,
            DateTime.UtcNow);
    }
}


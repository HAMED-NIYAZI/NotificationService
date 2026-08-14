using global::Notification.Domain.Exceptions;

namespace Notification.Domain.Entities;



public sealed class Notification
{
    private Notification()
    {
    }

    private Notification(
        Guid id,
        Guid applicationId,
        string recipientId,
        string? type,
        string? title,
        string? message,
        DateTime? ExpireAt)
    {
        if (id == Guid.Empty)
            throw new NotificationDomainException(
                "Notification id cannot be empty.");

        if ( applicationId==Guid.Empty)
            throw new NotificationDomainException(
                "Application id cannot be empty.");

        if (string.IsNullOrWhiteSpace(recipientId))
            throw new NotificationDomainException(
                "Recipient id is required.");

        if (string.IsNullOrWhiteSpace(type))
            throw new NotificationDomainException(
                "Notification type is required.");

        if (string.IsNullOrWhiteSpace(title))
            throw new NotificationDomainException(
                "Notification title is required.");

        if (string.IsNullOrWhiteSpace(message))
            throw new NotificationDomainException(
                "Notification message is required.");

        Id = id;
        ApplicationId = applicationId;
        RecipientId = recipientId;
        Type = type;
        Title = title;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid ApplicationId { get; private set; }

    public string RecipientId { get; private set; }  

    public string? Type { get; private set; }  

    public string? Title { get; private set; } 

    public string? Message { get; private set; }

    public DateTime? ExpireAt { get; private set; }
    public DateTime CreatedAt { get; private set; }= DateTime.Now;

    public NotificationApplication Application { get; private set; } = null!;

    public ICollection<NotificationDelivery> Deliveries { get; private set; }
        = new List<NotificationDelivery>();

    public static Notification Create(
        Guid applicationId,
        string? recipientId,
        string? type,
        string? title,
        string? message,
        DateTime? ExpireAt)
    {
        return new Notification(
            Guid.NewGuid(),
            applicationId,
            recipientId,
            type,
            title,
            message ,
            ExpireAt);
    }
}
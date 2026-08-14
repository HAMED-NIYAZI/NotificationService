namespace Notification.Domain.Exceptions;

public sealed class NotificationDomainException
    : DomainException
{
    public NotificationDomainException(string message)
        : base(message)
    {
    }
}
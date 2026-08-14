namespace Notification.Domain.Exceptions;


 
public sealed class NotificationDeliveryNotFoundException
    : DomainException
{
    public NotificationDeliveryNotFoundException(
        Guid deliveryId)
        : base(
            $"Notification delivery '{deliveryId}' was not found.")
    {
    }
}
 
public sealed class ApplicationIdNotFoundException
    : DomainException
{
    public ApplicationIdNotFoundException()
        : base(
            $"ApplicationId was not found.")
    {
    }
}

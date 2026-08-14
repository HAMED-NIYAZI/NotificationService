namespace Notification.Application.Abstractions.Acknowledgement;

public interface IDeliveryAcknowledgementService
{
    Task AcknowledgeDeliveredAsync(
        Guid deliveryId,
        string applicationId,
        string recipientId,
        string connectionId
        );

    Task MarkAsReadAsync(
        Guid deliveryId,
        string applicationId,
        string recipientId,
        string connectionId
        );
}
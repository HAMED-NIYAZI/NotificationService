namespace Notification.Application.Abstractions.Acknowledgement;

public interface IDeliveryAcknowledgementService
{
    Task AcknowledgeDeliveredAsync(
        Guid deliveryId,
        Guid applicationId,
        string recipientId,
        string connectionId
        );

    Task MarkAsReadAsync(
        Guid deliveryId,
        Guid applicationId,
        string recipientId,
        string connectionId
        );
}
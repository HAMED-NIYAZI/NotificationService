using Notification.Application.Abstractions.Acknowledgement;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain.Exceptions;

namespace Notification.Application.Features.Acknowledgement;

public sealed class DeliveryAcknowledgementService
    : IDeliveryAcknowledgementService
{
     private readonly INotificationDeliveryRepository _deliveryRepository;

    public DeliveryAcknowledgementService(
        INotificationDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    public async Task AcknowledgeDeliveredAsync(
        Guid deliveryId,
        Guid applicationId,
        string recipientId,
        string connectionId
        )
    {
        var delivery =
            await _deliveryRepository.GetForAcknowledgementAsync(
                deliveryId,
                applicationId,
                recipientId,
                connectionId
             );

        if (delivery is null)
        {
            throw new DomainException("Notification delivery was not found.");
        }

        await _deliveryRepository.MarkAsDeliveredAsync(
            deliveryId
            );
    }

    public async Task MarkAsReadAsync(
        Guid deliveryId,
        Guid applicationId,
        string recipientId,
        string connectionId
        )
    {
        var delivery =
            await _deliveryRepository.GetForAcknowledgementAsync(
                deliveryId,
                applicationId,
                recipientId,
                connectionId
                );

        if (delivery is null)
        {
            throw new DomainException("Notification delivery was not found.");
        }

        await _deliveryRepository.MarkAsReadAsync(
            deliveryId
            );
    }
}
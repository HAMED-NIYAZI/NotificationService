using Notification.Application.Features.Abstractions.Channels;
using Notification.Application.Features.Abstractions.Realtime;
using Notification.Domain.Entities.Enums;
using Microsoft.AspNetCore.SignalR;


namespace Notification.Infrastructure.SignalR;

public sealed class SignalRNotificationChannel
    : IRealtimeNotificationSender
{
    private readonly IHubContext<NotificationHub>
        _hubContext;

    private readonly IClientConnectionRegistry
        _registry;

    public SignalRNotificationChannel(
        IHubContext<NotificationHub> hubContext,
        IClientConnectionRegistry registry)
    {
        _hubContext = hubContext;
        _registry = registry;
    }

    public NotificationChannel Channel =>
        NotificationChannel.SignalR;

    public async Task<DeliveryResult> SendAsync(
        NotificationDeliveryMessage message,
        CancellationToken cancellationToken)
    {
        var connections =
            await _registry.GetConnectionsAsync(
                message.RecipientId,
                cancellationToken);

        if (connections.Count == 0)
        {
            return DeliveryResult.Failed(
                "Recipient has no active SignalR connections.");
        }

        await _hubContext.Clients
            .Clients(connections)
            .SendAsync(
                "notification",
                new
                {
                    deliveryId =
                        message.DeliveryId,

                    notificationId =
                        message.NotificationId,

                    type =
                        message.Type,

                    title =
                        message.Title,

                    message =
                        message.Message
                },
                cancellationToken);

        return DeliveryResult.Succeeded(
            requiresAcknowledgement: true);
    }

    public async Task SendAsync(IReadOnlyCollection<string> connectionIds, object notification, CancellationToken cancellationToken)
    {
        var connections =
            await _registry.GetConnectionsAsync(
                message.RecipientId,
                cancellationToken);

        if (connections.Count == 0)
        {
            return DeliveryResult.Failed(
                "Recipient has no active SignalR connections.");
        }

        await _hubContext.Clients
            .Clients(connections)
            .SendAsync(
                "notification",
                new
                {
                    deliveryId =
                        message.DeliveryId,

                    notificationId =
                        message.NotificationId,

                    type =
                        message.Type,

                    title =
                        message.Title,

                    message =
                        message.Message
                },
                cancellationToken);

        return DeliveryResult.Succeeded(
            requiresAcknowledgement: true);
    }
}

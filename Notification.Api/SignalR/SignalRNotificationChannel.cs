using Microsoft.AspNetCore.SignalR;
using Notification.Application.Abstractions.Channels;

namespace Notification.Api.SignalR;

public sealed class SignalRNotificationChannel
    : INotificationChannel
{
    private readonly IHubContext<NotificationHub>
        _hubContext;

    public SignalRNotificationChannel(
        IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendAsync(
        NotificationDeliveryMessage message
        )
    {
        await _hubContext.Clients
            .Client(message.ConnectionId)
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
                        message.Message,

                    createdAt =
                        message.CreatedAt
                }
                );
    }
}
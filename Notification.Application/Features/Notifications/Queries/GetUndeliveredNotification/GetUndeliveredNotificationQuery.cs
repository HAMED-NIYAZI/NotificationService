using MediatR;

namespace Notification.Application.Features.Notifications.Queries.GetUndeliveredNotification;

public sealed record GetUndeliveredNotificationQuery(
    Guid ApplicationId,
    Guid NotificationId) : IRequest<GetUndeliveredNotificationResult>;



using MediatR;

namespace Notification.Application.Features.Notifications.Commands.Create;

public sealed record CreateNotificationCommand(
    Guid ApplicationId,
    string? RecipientId,
    string? Type,
    string? Title,
    string? Message,
    DateTime? ExpireAt) : IRequest<CreateNotificationResult>;


 
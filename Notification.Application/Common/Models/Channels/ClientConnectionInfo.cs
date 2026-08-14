namespace Notification.Application.Common.Models.Channels;

public sealed record ClientConnectionInfo(
    Guid ApplicationId,
    string RecipientId,
    string ConnectionId);
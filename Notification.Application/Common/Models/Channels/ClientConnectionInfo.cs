namespace Notification.Application.Common.Models.Channels;

public sealed record ClientConnectionInfo(
    string ApplicationId,
    string RecipientId,
    string ConnectionId);
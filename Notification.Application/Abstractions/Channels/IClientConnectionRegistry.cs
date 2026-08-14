


using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Channels;

public interface IClientConnectionRegistry
{
    Task RegisterAsync(
        Guid applicationId,
        string recipientId,
        string connectionId
        );

    Task UnregisterAsync(
        string connectionId
        );

    Task<IReadOnlyList<ClientConnection>>
         GetActiveConnectionsAsync(
             Guid applicationId,
             string recipientId
             );

    Task<bool> IsOnlineAsync(
        Guid applicationId,
        string recipientId
        );
}
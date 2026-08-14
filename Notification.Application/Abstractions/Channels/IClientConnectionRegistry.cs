


using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Channels;

public interface IClientConnectionRegistry
{
    Task RegisterAsync(
        string applicationId,
        string recipientId,
        string connectionId
        );

    Task UnregisterAsync(
        string connectionId
        );

    Task<IReadOnlyList<ClientConnection>>
         GetActiveConnectionsAsync(
             string applicationId,
             string recipientId
             );

    Task<bool> IsOnlineAsync(
        string applicationId,
        string recipientId
        );
}
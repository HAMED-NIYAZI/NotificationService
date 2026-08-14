using System.Collections.Concurrent;
using Notification.Application.Abstractions.Channels;
using Notification.Application.Common.Models.Channels;
using Notification.Domain.Entities;

namespace Notification.Infrastructure.SignalR;

public   class InMemoryClientConnectionRegistry
    : IClientConnectionRegistry
{
    private readonly ConcurrentDictionary<
        string,
        ClientConnectionInfo> _connections = new();

    public Task RegisterAsync(
        Guid applicationId,
        string recipientId,
        string connectionId
        )
    {
        var connection =
            new ClientConnectionInfo(
                applicationId,
                recipientId,
                connectionId);

        _connections[connectionId] =
            connection;

        return Task.CompletedTask;
    }

    public Task UnregisterAsync(
        string connectionId
        )
    {
        _connections.TryRemove(
            connectionId,
            out _);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<string>>
        GetConnectionsAsync(
            Guid applicationId,
            string recipientId
            )
    {
        var result =
            _connections
                .Values
                .Where(x =>
                    x.ApplicationId == applicationId &&
                    x.RecipientId == recipientId)
                .Select(x => x.ConnectionId)
                .ToList();

        return Task.FromResult<
            IReadOnlyList<string>>(result);
    }

    public Task<bool> IsOnlineAsync(
        Guid applicationId,
        string recipientId
        )
    {
        var exists =
            _connections.Values.Any(x =>
                x.ApplicationId == applicationId &&
                x.RecipientId == recipientId);

        return Task.FromResult(exists);
    }
 

    public Task<IReadOnlyList<ClientConnection>> GetActiveConnectionsAsync(Guid applicationId, string recipientId)
    {
        throw new NotImplementedException();
    }


}
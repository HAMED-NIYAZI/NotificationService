using Notification.Application.Abstractions.Channels;
using Notification.Application.Abstractions.Persistence;
 using Notification.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Notification.Persistence.Channels;

public sealed class ClientConnectionRegistry
    : IClientConnectionRegistry
{
    private readonly IClientConnectionRepository _Repository;

    public ClientConnectionRegistry(
        IClientConnectionRepository Repository)
    {
        _Repository = Repository;
    }

    public async Task<ClientConnection> RegisterAsync(
        string applicationId,
        string recipientId,
        string connectionId
        )
    {
        var existing =
            await _Repository.GetByConnectionIdAsync(
                connectionId
                );

        if (existing is not null)
        {
            existing.Reconnect();

            await _Repository.UpdateAsync(
                existing
                );

            return existing;
        }

        var connection =
            ClientConnection.Create(
                applicationId,
                recipientId,
                connectionId);

        await _Repository.AddAsync(
            connection
            );

        return connection;
    }

    public async Task UnregisterAsync(
        string connectionId
        )
    {
        var connection =
            await _Repository.GetByConnectionIdAsync(
                connectionId
                );

        if (connection is null)
            return;

        connection.Disconnect();

        await _Repository.UpdateAsync(
            connection
            );
    }

    public Task<IReadOnlyList<ClientConnection>>
        GetActiveConnectionsAsync(
            string applicationId,
            string recipientId
            )
    {
        return _Repository.GetActiveAsync(
         applicationId, recipientId
            );
    }

 
 

    public Task<bool> IsOnlineAsync(string applicationId, string recipientId)
    {
        throw new NotImplementedException();
    }

    Task IClientConnectionRegistry.RegisterAsync(string applicationId, string recipientId, string connectionId)
    {
        return RegisterAsync(applicationId, recipientId, connectionId);
    }
}
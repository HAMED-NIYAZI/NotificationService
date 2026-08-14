using Notification.Domain.Entities;

namespace Notification.Application.Abstractions.Persistence;

public interface IClientConnectionRepository
{
    Task AddAsync(
        ClientConnection connection
        );

    Task<ClientConnection?> GetByConnectionIdAsync(
        string connectionId
        );

    Task<IReadOnlyList<ClientConnection>>
        GetActiveAsync(
            Guid applicationId,
            string recipientId
            );

    Task UpdateAsync(
        ClientConnection connection
        );
}
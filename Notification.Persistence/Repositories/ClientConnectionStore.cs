using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain.Entities;
using Notification.Persistence.Context;

namespace Notification.Persistence.Repositories;

public sealed class ClientConnectionRepository
    : IClientConnectionRepository
{
    private readonly NotificationDbContext _dbContext;

    public ClientConnectionRepository(
        NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        ClientConnection connection
        )
    {
        await _dbContext.ClientConnections.AddAsync(
            connection
            );
    }

    public async Task<ClientConnection?> GetByConnectionIdAsync(
        string connectionId
        )
    {
        return await _dbContext.ClientConnections
            .FirstOrDefaultAsync(
                x => x.ConnectionId == connectionId
                );
    }

    public async Task<IReadOnlyList<ClientConnection>>
        GetActiveAsync(
            Guid applicationId,
            string recipientId
            )
    {
        return await _dbContext.ClientConnections
            .Where(x =>
                x.ApplicationId == applicationId &&
                x.RecipientId == recipientId &&
                x.IsActive)
            .ToListAsync();
    }

    public Task UpdateAsync(
        ClientConnection connection
        )
    {
        _dbContext.ClientConnections.Update(connection);

        return Task.CompletedTask;
    }
}
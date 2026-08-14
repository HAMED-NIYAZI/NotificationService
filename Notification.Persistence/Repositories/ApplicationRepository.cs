using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain.Entities;
using Notification.Persistence.Context;

namespace Notification.Persistence.Repositories;

public sealed class ApplicationRepository
    : IApplicationRepository
{
    private readonly NotificationDbContext _dbContext;

    public ApplicationRepository(
        NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotificationApplication?>
        GetByApiKeyAsync(
            string apiKey
            )
    {
        return await _dbContext.NotificationApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ApiKey == apiKey
                     && x.IsActive
                );
    }
}
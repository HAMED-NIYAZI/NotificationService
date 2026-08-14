using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain.Entities;
using Notification.Domain.Entities.Enums;
using Notification.Persistence.Context;
using System.Data;

namespace Notification.Persistence.Repositories;

public sealed class NotificationDeliveryRepository
    : INotificationDeliveryRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationDeliveryRepository(
        NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> MarkAsDeliveredAsync(
        Guid deliveryId
        )
    {
        var now = DateTime.UtcNow;

        var affectedRows =
            await _dbContext.Database
                .ExecuteSqlInterpolatedAsync(
                    $"""
                UPDATE dbo.NotificationDeliveries
                SET
                    Status =
                        {(byte)NotificationDeliveryStatus.Delivered},
                    DeliveredAt = {now},
                    UpdatedAt = {now}
                WHERE
                    Id = {deliveryId}
                    AND Status =
                        {(byte)NotificationDeliveryStatus.Sent}
                """
                    );

        return affectedRows > 0;
    }

    public async Task<bool> MarkAsFailedAsync(
       Guid deliveryId,
       Guid leaseId,
       string error
       )
    {
        var connection =
            _dbContext.Database.GetDbConnection();

        await using var command =
            connection.CreateCommand();

        command.CommandText =
            "dbo.MarkNotificationDeliveryFailed";

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            new SqlParameter(
                "@DeliveryId",
                SqlDbType.UniqueIdentifier)
            {
                Value = deliveryId
            });

        command.Parameters.Add(
            new SqlParameter(
                "@LeaseId",
                SqlDbType.UniqueIdentifier)
            {
                Value = leaseId
            });

        command.Parameters.Add(
            new SqlParameter(
                "@Error",
                SqlDbType.NVarChar,
                4000)
            {
                Value = error
            });

        command.Parameters.Add(
            new SqlParameter(
                "@MaxRetryCount",
                SqlDbType.Int)
            {
                Value = 8
            });

        command.Parameters.Add(
            new SqlParameter(
                "@MaxRetryDelaySeconds",
                SqlDbType.Int)
            {
                Value = 300
            });

        if (connection.State !=
            ConnectionState.Open)
        {
            await connection.OpenAsync(
                );
        }

        await command.ExecuteNonQueryAsync(
            );
        return true;
    }
    public async Task<bool> MarkAsReadAsync(
        Guid deliveryId
        )
    {
        var now = DateTime.UtcNow;

        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
        UPDATE dbo.NotificationDeliveries
        SET
            Status = {(byte)NotificationDeliveryStatus.Read},
            ReadAt = {now},
            UpdatedAt = {now}
        WHERE
            Id = {deliveryId}
            AND Status IN
            (
                {(byte)NotificationDeliveryStatus.Sent},
                {(byte)NotificationDeliveryStatus.Delivered}
            )
        """
            );
        return true;
    }

    public async Task<bool> MarkAsSentAsync(
        Guid deliveryId,
        Guid leaseId
        )
    {
        var now = DateTime.UtcNow;

        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
            UPDATE dbo.NotificationDeliveries
            SET
                Status = 3,
                SentAt = {now},
                LeaseId = NULL,
                LeaseUntil = NULL,
                UpdatedAt = {now}
            WHERE
                Id = {deliveryId}
                AND Status = 2
                AND LeaseId = {leaseId}
            """
            );
        return true;
    }


    public async Task<NotificationDelivery?>
        GetForAcknowledgementAsync(
            Guid deliveryId,
            string applicationId,
            string recipientId,
            string connectionId
            )
    {
        return await _dbContext.NotificationDeliveries
            .Include(x => x.ClientConnection)
            .Include(x => x.Notification)
            .FirstOrDefaultAsync(
                x =>
                    x.Id == deliveryId
                    &&
                    x.ClientConnection.ApplicationId ==
                        applicationId
                    &&
                    x.ClientConnection.RecipientId ==
                        recipientId
                    &&
                    x.ClientConnection.ConnectionId ==
                        connectionId
                    &&
                    x.ClientConnection.IsActive
                );
    }
    public Task AddRangeAsync(IReadOnlyCollection<NotificationDelivery> deliveries)
    {
        throw new NotImplementedException();
    }

    public async Task<NotificationDelivery?> ClaimNextAsync(
        Guid leaseId,
        TimeSpan leaseDuration
        )
    {
        var connection =
            _dbContext.Database.GetDbConnection();

        await using var command =
            connection.CreateCommand();

        command.CommandText =
            "dbo.ClaimNextNotificationDelivery";

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            new SqlParameter(
                "@LeaseId",
                SqlDbType.UniqueIdentifier)
            {
                Value = leaseId
            });

        command.Parameters.Add(
            new SqlParameter(
                "@LeaseDurationSeconds",
                SqlDbType.Int)
            {
                Value = (int)leaseDuration.TotalSeconds
            });

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(
                );
        }

        var result =
            await command.ExecuteScalarAsync(
                );

        if (result is null)
            return null;

        var deliveryId =
            (Guid)result;

        return await _dbContext.NotificationDeliveries
            .Include(x => x.Notification)
            .Include(x => x.ClientConnection)
            .FirstAsync(
                x => x.Id == deliveryId
                );
    }

 


    public async Task<IReadOnlyList<NotificationDelivery>>
    GetPendingForRecipientAsync(
        string applicationId,
        string recipientId
        )
    {
        return await _dbContext.NotificationDeliveries
            .Include(x => x.Notification)
            .Include(x => x.ClientConnection)
            .Where(x =>
                x.ClientConnection.ApplicationId ==
                    applicationId
                &&
                x.ClientConnection.RecipientId ==
                    recipientId
                &&
                x.Status == NotificationDeliveryStatus.Pending)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

 

 
 
}
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Queue;
using Notification.Domain.Entities.Enums;
using Notification.Persistence.Context;
using System.Data;

namespace Notification.Persistence.Queues;

public sealed class SqlNotificationQueue
    : INotificationQueue
{
    private readonly NotificationDbContext _dbContext;

    public SqlNotificationQueue(
        NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<
        IReadOnlyList<NotificationDeliveryWorkItem>>
        ClaimAsync(
            int batchSize
            )
    {
        var leaseId = Guid.NewGuid();

        var command = _dbContext.Database
            .GetDbConnection()
            .CreateCommand();

        command.CommandText =
            "dbo.ClaimNotificationDeliveries";

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            new SqlParameter(
                "@BatchSize",
                SqlDbType.Int)
            {
                Value = batchSize
            });

        command.Parameters.Add(
            new SqlParameter(
                "@LeaseDurationSeconds",
                SqlDbType.Int)
            {
                Value = 60
            });

        command.Parameters.Add(
            new SqlParameter(
                "@LeaseId",
                SqlDbType.UniqueIdentifier)
            {
                Value = leaseId
            });

        if (command.Connection!.State !=
            ConnectionState.Open)
        {
            await command.Connection.OpenAsync(
                );
        }

        var result =
            new List<NotificationDeliveryWorkItem>();

        await using var reader =
            await command.ExecuteReaderAsync(
                );

        while (await reader.ReadAsync(
                   ))
        {
            result.Add(
                new NotificationDeliveryWorkItem
                {
                    DeliveryId =
                        reader.GetGuid(
                            reader.GetOrdinal("Id")),

                    NotificationId =
                        reader.GetGuid(
                            reader.GetOrdinal(
                                "NotificationId")),

                    RecipientId =
                        reader.GetString(
                            reader.GetOrdinal(
                                "RecipientId")),

                    ClientId =
                        reader.GetGuid(
                            reader.GetOrdinal(
                                "ClientId")),

                    Channel =
                        (NotificationChannel)
                        reader.GetByte(
                            reader.GetOrdinal(
                                "Channel")),

                    AttemptCount =
                        reader.GetInt32(
                            reader.GetOrdinal(
                                "AttemptCount")),

                    LeaseId =
                        reader.GetGuid(
                            reader.GetOrdinal(
                                "LeaseId")),

                    LeaseUntil =
                        reader.GetDateTime(
                            reader.GetOrdinal(
                                "LeaseUntil"))
                });
        }

        return result;
    }
}
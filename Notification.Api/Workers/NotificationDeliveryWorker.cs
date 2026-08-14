using Notification.Application.Abstractions.Channels;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Features.Deliveries.Send;

namespace Notification.Api.Workers;

public sealed class NotificationDeliveryWorker
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<NotificationDeliveryWorker> _logger;

    public NotificationDeliveryWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<NotificationDeliveryWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
       CancellationToken  stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope =
                    _scopeFactory.CreateScope();

                var Repository =
                    scope.ServiceProvider
                        .GetRequiredService<
                            INotificationDeliveryRepository>();

                var sender =
                    scope.ServiceProvider
                        .GetRequiredService<
                            NotificationDeliverySender>();

                var leaseId = Guid.NewGuid();

                var delivery =
                    await Repository.ClaimNextAsync(
                        leaseId,
                        TimeSpan.FromSeconds(30)
                        );

                if (delivery is null)
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(1),
                        stoppingToken);

                    continue;
                }

                var message =
                    new NotificationDeliveryMessage(
                        delivery.Id,
                        delivery.NotificationId,
                        delivery.ClientConnection.ConnectionId,
                        delivery.Notification.Type,
                        delivery.Notification.Title,
                        delivery.Notification.Message,
                        delivery.Notification.CreatedAt);
          await sender.SendAsync(
                    message,
                    leaseId
                    );
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Notification delivery worker failed.");

                await Task.Delay(
                    TimeSpan.FromSeconds(2),
                    stoppingToken);
            }
        }
    }
}
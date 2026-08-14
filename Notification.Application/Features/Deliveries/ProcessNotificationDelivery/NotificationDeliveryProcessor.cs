using Notification.Application.Abstractions.Channels;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Abstractions.Queue;

namespace Notification.Application.Features.Deliveries.ProcessNotificationDelivery
{
    public class NotificationDeliveryProcessor
        : INotificationDeliveryProcessor
    {
        private readonly INotificationQueue _queue;

        private readonly INotificationDeliveryStore _store;

        private readonly IEnumerable<INotificationChannel>
            _channels;

        public NotificationDeliveryProcessor(
            INotificationQueue queue,
            INotificationDeliveryStore store,
            IEnumerable<INotificationChannel> channels)
        {
            _queue = queue;
            _store = store;
            _channels = channels;
        }

        public async Task ProcessBatchAsync(
            CancellationToken cancellationToken)
        {
            var deliveries =
                await _queue.ClaimAsync(
                    50,
                    cancellationToken);

            foreach (var delivery in deliveries)
            {
                await ProcessAsync(
                    delivery,
                    cancellationToken);
            }
        }

        private async Task ProcessAsync(
            NotificationDeliveryWorkItem delivery,
            CancellationToken cancellationToken)
        {
            try
            {
                var channel =
                    _channels.SingleOrDefault(
                        x =>
                            x.Channel ==
                            delivery.Channel);

                if (channel is null)
                {
                    await _store.MarkAsFailedAsync(
                        delivery.DeliveryId,
                        delivery.LeaseId,
                        $"Channel '{delivery.Channel}' not found.",
                        cancellationToken);

                    return;
                }

                var message =
                    new NotificationDeliveryMessage
                    {
                        DeliveryId =
                            delivery.DeliveryId,

                        NotificationId =
                            delivery.NotificationId,

                        ConnectionId =
                            delivery.ConnectionId,

                       
                       
                       
                        Type =
                            delivery.Type,

                        Title =
                            delivery.Title,

                        Message =
                            delivery.Message
 


                    };

                var result =
                    await channel.SendAsync(
                        message,
                        cancellationToken);

                if (result.Success)
                {
                    await _store.MarkAsSentAsync(
                        delivery.DeliveryId,
                        delivery.LeaseId,
                        cancellationToken);

                    return;
                }

                await _store.MarkAsFailedAsync(
                    delivery.DeliveryId,
                    delivery.LeaseId,
                    result.Error ??
                    "Notification delivery failed.",
                    cancellationToken);
            }
            catch (Exception ex)
            {
                await _store.MarkAsFailedAsync(
                    delivery.DeliveryId,
                    delivery.LeaseId,
                    ex.Message,
                    cancellationToken);
            }
        }
    }
}

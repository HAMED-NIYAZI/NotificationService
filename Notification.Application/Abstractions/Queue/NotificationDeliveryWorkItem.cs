using Notification.Domain.Entities.Enums;

namespace Notification.Application.Abstractions.Queue
{
    public class NotificationDeliveryWorkItem
    {
        public   Guid DeliveryId { get; init; }
                
        public   Guid NotificationId { get; init; }
                
        public   string RecipientId { get; init; }
                
        public   Guid ClientId { get; init; }
                
        public   NotificationChannel Channel { get; init; }
                
        public   string Type { get; init; }
                
        public   string Title { get; init; }
                
        public   string Message { get; init; }
                
        public   int AttemptCount { get; init; }
                
        public   Guid LeaseId { get; init; }
                
        public   DateTime LeaseUntil { get; init; }
    }
}

 
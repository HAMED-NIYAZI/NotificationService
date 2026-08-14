namespace Notification.Application.Features.Notifications.Commands.Create
{
    public sealed class CreateNotificationRequest
    {
        public string? RecipientId { get; set; }

        public string? Type { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }
        public DateTime? ExpireAt { get; set; } 
    }
}

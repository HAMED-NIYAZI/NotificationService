namespace Notification.Application.Abstractions.Realtime
{
    public interface IRealtimeNotificationSender
    {
        Task SendAsync(IReadOnlyCollection<string> connectionIds, object notification);
    }
}

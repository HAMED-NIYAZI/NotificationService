namespace Notification.Application.Abstractions.Persistence;

public interface INotificationRepository
{
    Task AddAsync(Domain.Entities.Notification notification);

    Task<Domain.Entities.Notification> GetAsync(Guid applicationId, Guid notificationId);
    Task<IReadOnlyList<Domain.Entities.Notification>> GetDeliveredAsync(Guid applicationId, string recipientId);
    Task<IReadOnlyList<Domain.Entities.Notification>> GetUndeliveredAsync(Guid applicationId, string recipientId);
    Task<IReadOnlyList<Domain.Entities.Notification>> GetUnReadAsync(Guid applicationId, string recipientId);
    Task<IReadOnlyList<Domain.Entities.Notification>> GetReadAsync(Guid applicationId, string recipientId);

}
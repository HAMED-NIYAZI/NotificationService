using Notification.Domain.Exceptions;

namespace Notification.Domain.Entities;


public class ClientConnection
{
    private ClientConnection()
    {
    }

    public ClientConnection(
        Guid id,
        string applicationId,
        string recipientId,
        string connectionId)
    {
        if (id == Guid.Empty)
            throw new NotificationDomainException(
                "Connection id cannot be empty.");

        if (string.IsNullOrEmpty( applicationId))
            throw new NotificationDomainException(
                "Application id cannot be empty.");

        if (string.IsNullOrWhiteSpace(recipientId))
            throw new NotificationDomainException(
                "Recipient id is required.");

        if (string.IsNullOrWhiteSpace(connectionId))
            throw new NotificationDomainException(
                "SignalR connection id is required.");

        Id = id;
        ApplicationId = applicationId;
        RecipientId = recipientId;
        ConnectionId = connectionId;

        IsActive = true;
        ConnectedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string ApplicationId { get; private set; }

    public string RecipientId { get; private set; } = null!;

    public string ConnectionId { get; private set; } = null!;

    public bool IsActive { get; set; }

    public DateTime ConnectedAt { get; private set; }

    public DateTime? DisconnectedAt { get; private set; }

    public NotificationApplication Application { get; private set; } = null!;

    public ICollection<NotificationDelivery> Deliveries { get; private set; }
        = new List<NotificationDelivery>();

    public static ClientConnection Create(
        string applicationId,
        string recipientId,
        string connectionId)
    {
        return new ClientConnection(
            Guid.NewGuid(),
            applicationId,
            recipientId,
            connectionId);
    }

    public void Disconnect()
    {
        IsActive = false;
        if (!IsActive)
            return;

        IsActive = false;
        DisconnectedAt = DateTime.UtcNow;
    }

    public void Reconnect()
    {
        IsActive = true;
        DisconnectedAt = null;
    }
}
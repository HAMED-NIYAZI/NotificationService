namespace Notification.Domain.Entities;

public class PushSubscription
{
    private PushSubscription()
    {
    }

    private PushSubscription(
        Guid id,
        Guid clientId,
        string endpoint,
        string p256dh,
        string auth,
        DateTime createdAt,
        DateTime? expiresAt)
    {
        Id = id;
        ClientId = clientId;
        Endpoint = endpoint;
        P256dh = p256dh;
        Auth = auth;
        IsActive = true;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public Guid Id { get; private set; }

    public Guid ClientId { get; private set; }

    public string Endpoint { get; private set; } = null!;

    public string P256dh { get; private set; } = null!;

    public string Auth { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    public ClientConnection Client { get; private set; } = null!;

    public static PushSubscription Create(
        Guid clientId,
        string endpoint,
        string p256dh,
        string auth,
        DateTime? expiresAt = null)
    {
        if (clientId == Guid.Empty)
            throw new ArgumentException(
                "ClientId is required.");

        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException(
                "Endpoint is required.");

        return new PushSubscription(
            Guid.NewGuid(),
            clientId,
            endpoint,
            p256dh,
            auth,
            DateTime.UtcNow,
            expiresAt);
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
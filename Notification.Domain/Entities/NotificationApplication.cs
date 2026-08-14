using Notification.Domain.Exceptions;

namespace Notification.Domain.Entities;

  
public sealed class NotificationApplication
{
    private NotificationApplication()
    {
    }

    private NotificationApplication(
        Guid id,
        string name,
        Guid apiKey)
    {
        if (id == Guid.Empty)
            throw new NotificationDomainException(
                "Application id cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new NotificationDomainException(
                "Application name is required.");

        if (apiKey==Guid.Empty)
            throw new NotificationDomainException(
                "Api key hash is required.");

        Id = id;
        Name = name;
        ApiKey= apiKey;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Guid ApiKey { get; private set; } = Guid.Empty!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public ICollection<Notification> Notifications { get; private set; }
        = new List<Notification>();

    public ICollection<ClientConnection> ClientConnections { get; private set; }
        = new List<ClientConnection>();

    public static NotificationApplication Create(
        string name,
        Guid ApiKey)
    {
        return new NotificationApplication(
            Guid.NewGuid(),
            name,
            ApiKey);
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
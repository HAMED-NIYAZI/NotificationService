namespace Notification.Domain.Entities;

public class OutboxMessage
{
    private OutboxMessage()
    {
    }

    private OutboxMessage(
        Guid id,
        string type,
        string payload)
    {
        Id = id;
        Type = type;
        Payload = payload;
        CreatedAt = DateTime.UtcNow;
        AttemptCount = 0;
    }

    public Guid Id { get; private set; }

    public string Type { get; private set; } = null!;

    public string Payload { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime? ProcessedAt { get; private set; }

    public int AttemptCount { get; private set; }

    public string? LastError { get; private set; }

    public static OutboxMessage Create(
        string type,
        string payload)
    {
        return new OutboxMessage(
            Guid.NewGuid(),
            type,
            payload);
    }

    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
    }

    public void RegisterFailure(string error)
    {
        AttemptCount++;
        LastError = error;
    }
}
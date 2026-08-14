using Microsoft.AspNetCore.SignalR;
using Notification.Application.Abstractions.Acknowledgement;
using Notification.Application.Abstractions.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public sealed class NotificationHub : Hub
{
    private readonly IClientConnectionRegistry _registry;
    private readonly IDeliveryAcknowledgementService _acknowledgementService;

    public NotificationHub(
        IClientConnectionRegistry registry,
        IDeliveryAcknowledgementService acknowledgementService)
    {
        _registry = registry;
        _acknowledgementService = acknowledgementService;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();

        string strapplicationId =
            httpContext?
                .Request
                .Query["applicationId"]
                .FirstOrDefault();

        var recipientId =
            httpContext?
                .Request
                .Query["recipientId"]
                .FirstOrDefault();
        Guid.TryParse(strapplicationId, out var applicationId);

        if (applicationId == Guid.Empty)
        {
            throw new HubException(
                "applicationId is required.");
        }

        if (string.IsNullOrWhiteSpace(recipientId))
        {
            throw new HubException(
                "recipientId is required.");
        }

        await _registry.RegisterAsync(
                   applicationId,
          recipientId,
          "");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        await _registry.UnregisterAsync(
            Context.ConnectionId
             );

        await base.OnDisconnectedAsync(exception);
    }

    public async Task AcknowledgeDelivered(
        Guid deliveryId)
    {
        var context = GetClientContext();

        await _acknowledgementService
            .AcknowledgeDeliveredAsync(
                deliveryId,
                context.ApplicationId,
                context.RecipientId,
                context.ConnectionId
                );
    }

    public async Task MarkAsRead(
        Guid deliveryId)
    {
        var context = GetClientContext();

        await _acknowledgementService
            .MarkAsReadAsync(
                deliveryId,
                context.ApplicationId,
                context.RecipientId,
                context.ConnectionId
                );
    }

    private ClientContext GetClientContext()
    {
        var httpContext = Context.GetHttpContext();

        string strapplicationId =
            httpContext?
                .Request
                .Query["applicationId"]
                .FirstOrDefault();

        string recipientId =
            httpContext?
                .Request
                .Query["recipientId"]
                .FirstOrDefault();
        Guid.TryParse(strapplicationId , out var applicationId);

        if (applicationId==Guid.Empty)
        {
            throw new HubException(
                "applicationId is required.");
        }

        if (string.IsNullOrWhiteSpace(recipientId))
        {
            throw new HubException(
                "recipientId is required.");
        }

        return new ClientContext(
            applicationId,
            recipientId,
            Context.ConnectionId);
    }

    private sealed record ClientContext(
        Guid ApplicationId,
        string RecipientId,
        string ConnectionId);
}
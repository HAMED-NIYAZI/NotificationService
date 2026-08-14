using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Features.Notifications.Commands.Create;
using Notification.Domain.Exceptions;

namespace Notification.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class NotificationsController : ControllerBase
{
    private readonly ISender _sender;

    public NotificationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        var command =
            new CreateNotificationCommand(
                (Guid)GetApikey(),
                request.RecipientId,
                request.Type,
                request.Title,
                request.Message,
                request.ExpireAt);




        var notificationId = await _sender.Send(command);

        return Ok(notificationId);
    }


    public Guid? GetApikey()
    {
        string? apiKey = Request.Headers["X-API-Key"];

        if (string.IsNullOrWhiteSpace(apiKey)) throw new ApplicationIdNotFoundException();

        if (Guid.TryParse(apiKey, out Guid parsedKey)) return parsedKey;

        throw new ApplicationIdNotFoundException();

    }

}
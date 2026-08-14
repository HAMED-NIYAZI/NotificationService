using MediatR;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Abstractions.Transaction;

namespace Notification.Application.Features.Notifications.Commands.Create;

public sealed class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResult>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateNotificationResult> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
    {

        var notification = 
        Domain.Entities.Notification.Create(
            command.ApplicationId,
            command.RecipientId,
            command.Type,
            command.Title,
            command.Message,
            command.ExpireAt);

        await _notificationRepository.AddAsync(notification);


        await _unitOfWork.SaveChangesAsync();
        return new CreateNotificationResult { NotificationId = notification.Id };
    }

 
}
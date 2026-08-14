using MediatR;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Abstractions.Transaction;

namespace Notification.Application.Features.Notifications.Queries.GetUndeliveredNotification;

public sealed class GetUndeliveredNotificationQueryHandler : IRequestHandler<GetUndeliveredNotificationQuery, GetUndeliveredNotificationResult>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetUndeliveredNotificationQueryHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUndeliveredNotificationResult> Handle(GetUndeliveredNotificationQuery Query ,  CancellationToken cancellationToken)
    {
        return new GetUndeliveredNotificationResult();


    }
 
}
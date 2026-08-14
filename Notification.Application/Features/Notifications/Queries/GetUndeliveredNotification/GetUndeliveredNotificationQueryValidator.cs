using FluentValidation;

namespace Notification.Application.Features.Notifications.Queries.GetUndeliveredNotification;

public class GetUndeliveredNotificationQueryValidator : AbstractValidator<GetUndeliveredNotificationQuery>
{
    public GetUndeliveredNotificationQueryValidator()
    {

        RuleFor(x => x.NotificationId).NotEmpty().WithMessage("کد پیام الزامی است");

     }
}

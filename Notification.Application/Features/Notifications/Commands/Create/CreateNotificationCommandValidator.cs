using FluentValidation;

namespace Notification.Application.Features.Notifications.Commands.Create;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {

        RuleFor(x => x.Message).NotEmpty().WithMessage("متن پیام الزامی است");

     }
}

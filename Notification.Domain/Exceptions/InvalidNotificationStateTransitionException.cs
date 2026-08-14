namespace Notification.Domain.Exceptions
{
    public sealed class InvalidNotificationStateTransitionException : Exception
    {
       public InvalidNotificationStateTransitionException(string message) : base(message)
        {
        }

    }
}

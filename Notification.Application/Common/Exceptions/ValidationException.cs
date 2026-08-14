namespace Notification.Application.Common.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IDictionary<string, string[]> errors)
        : base("اطلاعات واردشده معتبر نیست.")
    {
        Errors = errors;
        ErrorMessage = string.Join(
    ", ",
    errors.SelectMany(x => x.Value)
          .Distinct());
    }

    public IDictionary<string, string[]> Errors { get; }
    public string ErrorMessage { get; }

}




public class NotFoundException : Exception //404
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}

public class BadRequestException : Exception//400
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}

public class ForbiddenException : Exception //403
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}


public class UnauthorizedException : Exception //401
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}
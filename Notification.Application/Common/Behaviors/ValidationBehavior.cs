using FluentValidation;
using MediatR;

namespace Notification.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .GroupBy(
                error => error.PropertyName,
                error => error.ErrorMessage)
            .ToDictionary(
                group => group.Key,
                group => group.Distinct().ToArray());

        if (failures.Count != 0)
            throw new Notification.Application.Common.Exceptions.ValidationException(failures);

        return await next();
    }




}

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Persistence;

namespace Notification.Api.Authentication;

public sealed class ApiKeyAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string ApiKeyHeader = "X-Api-Key";

    private readonly IApplicationRepository _applicationRepository;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IApplicationRepository applicationRepository)
        : base(options, logger, encoder)
    {
        _applicationRepository = applicationRepository;
    }

    protected override async Task<AuthenticateResult>
        HandleAuthenticateAsync()
    {

       var apiKey = GetApiKey();

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.NoResult();
        }


        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.Fail(
                "API Key is required.");
        }

        var application =
            await _applicationRepository
                .GetByApiKeyAsync(
                    apiKey!);

        if (application is null)
        {
            return AuthenticateResult.Fail(
                "Invalid API Key.");
        }

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                application.Id.ToString()),

            new Claim(
                "application_id",
                application.Id.ToString()),

            new Claim(
                "application_name",
                application.Name)
        };

        var identity =
            new ClaimsIdentity(
                claims,
                Scheme.Name);

        var principal =
            new ClaimsPrincipal(identity);

        var ticket =
            new AuthenticationTicket(
                principal,
                Scheme.Name);



        return AuthenticateResult.Success(ticket);
    }



    private string? GetApiKey()
    {
        if (Request.Headers.TryGetValue(
                "X-Api-Key",
                out var apiKey))
        {
            return apiKey.FirstOrDefault();
        }

        var authorization =
            Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(authorization)
            &&
            authorization.StartsWith(
                "Bearer ",
                StringComparison.OrdinalIgnoreCase))
        {
            return authorization["Bearer ".Length..]
                .Trim();
        }

        return null;
    }
}
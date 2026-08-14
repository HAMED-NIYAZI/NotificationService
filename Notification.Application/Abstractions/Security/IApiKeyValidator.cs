namespace Notification.Application.Abstractions.Security;

public interface IApiKeyValidator
{
    string Hash(string apiKey);
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions.Channels;
using Notification.Infrastructure.SignalR;

namespace Notification.Infrastructure
{

    public static class DependencyContainer
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
 

            services.AddScoped<IClientConnectionRegistry, InMemoryClientConnectionRegistry>();
 
        }
    }
}

using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions.Channels;
using Notification.Application.Common.Behaviors;
using Notification.Application.Common.Mappings.Mapster;
using Notification.Infrastructure.SignalR;
using System.Reflection;


namespace Notification.Infrastructure
{

    public static class DependencyContainer
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);



            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(typeof(UserMappingConfig).Assembly);

            services.AddScoped<IClientConnectionRegistry, InMemoryClientConnectionRegistry>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

 
  
        }
    }
}

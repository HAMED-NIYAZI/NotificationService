using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Abstractions.Queue;
using Notification.Application.Abstractions.Transaction;
using Notification.Persistence.Context;
using Notification.Persistence.Queues;
using Notification.Persistence.Repositories;

namespace Notification.Persistence
{

    public static class DependencyContainer
    {
        public static void RegisterPersistenceRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("NotificationServiceConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();
            services.AddScoped<INotificationQueue, SqlNotificationQueue>();
        }
    }
}

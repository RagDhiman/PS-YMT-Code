using AM_NotificationsService_Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_NotificationsService_Core
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}

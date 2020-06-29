using AM_NotificationsService_Data_EFC.Repositories;
using AM_NotificationsService_Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_NotificationsService_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddScoped<IEmailRepository>(serviceProvider => new EmailRepository(connectionString));
            services.AddScoped<ISMSRepository>(serviceProvider => new SMSRepository(connectionString));
            services.AddScoped<IWebhookPostRepository>(serviceProvider => new WebhookPostRepository(connectionString));

        }
    }
}

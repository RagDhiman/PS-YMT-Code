using AM_CustomerManager_Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace AM_CustomerManager_Core
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {   
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<INotificationService, NotificationService>();

            if (Configuration.GetValue<bool>("UseNewCustomerService"))
            {
                services.AddScoped<ICustomerService, CustomerServiceNew>();
            }
            else
            {
                services.AddScoped<ICustomerService, CustomerService>();
            }
        }
    }
}

using AccountsManager_Domain.DataAccess;
using AccountsManager_Domain.RemoteDataRepositories;
using AccountsManager_Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccountsManager_Domain
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICustomerCreditService, CustomerCreditService>();
        }
    }
}

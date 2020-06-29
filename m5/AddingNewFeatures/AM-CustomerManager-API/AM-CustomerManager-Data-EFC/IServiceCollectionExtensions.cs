using AM_CustomerManager_Core.DataAccess;
using AM_CustomerManager_Core.Services;
using AM_CustomerManager_Data_EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_CustomerManager_Data_EFC
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddDbContext<AccountManagerContext>(options =>
                options
                .UseSqlServer(connectionString)
                //.UseLoggerFactory(loggerFactory)
                );

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IPaymentBillingRepository, PaymentBillingRepository>();
            services.AddScoped<ITaxInfoRepository, TaxInfoRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAccountService, AccountService>();


            services.AddScoped<IBankAccountRepository, BankAccountRepository>();


        }
    }
}

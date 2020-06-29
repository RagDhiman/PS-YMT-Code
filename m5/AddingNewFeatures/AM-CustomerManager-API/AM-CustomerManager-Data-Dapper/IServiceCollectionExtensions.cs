using AM_CustomerManager_Core.DataAccess;
using AM_CustomerManager_Core.Services;
using AM_CustomerManager_Data_Dapper.Repositories;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace AM_CustomerManager_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {

            services.AddScoped<ICustomerRepository>(serviceProvider => new CustomerRepository(connectionString));
            services.AddScoped<IAddressRepository>(serviceProvider => new AddressRepository(connectionString));
            services.AddScoped<INoteRepository>(serviceProvider => new NoteRepository(connectionString));
            services.AddScoped<IPaymentBillingRepository>(serviceProvider => new PaymentBillingRepository(connectionString));
            services.AddScoped<ITaxInfoRepository>(serviceProvider => new TaxInfoRepository(connectionString));
            services.AddScoped<IBankAccountRepository>(serviceProvider => new BankAccountRepository(connectionString));
           
        }
    }
}

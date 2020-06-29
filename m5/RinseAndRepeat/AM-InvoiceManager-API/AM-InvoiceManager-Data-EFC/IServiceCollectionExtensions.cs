using AM_CustomerManager_Data_EFC.Repositories;
using AM_InvoiceManager_Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_InvoiceManager_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddScoped<ICreditNoteRepository>(serviceProvider => new CreditNoteRepository(connectionString));
            services.AddScoped<ICreditRepository>(serviceProvider => new CreditRepository(connectionString));
            services.AddScoped<IDelayedChargeRepository>(serviceProvider => new DelayedChargeRepository(connectionString));
            services.AddScoped<IEstimateRepository>(serviceProvider => new EstimateRepository(connectionString));
            services.AddScoped<IInvoiceLineRepository>(serviceProvider => new InvoiceLineRepository(connectionString));
            services.AddScoped<IInvoiceRepository>(serviceProvider => new InvoiceRepository(connectionString));
            services.AddScoped<IPaymentRepository>(serviceProvider => new PaymentRepository(connectionString));
            services.AddScoped<ISalesReceiptRepository>(serviceProvider => new SalesReceiptRepository(connectionString));
        }
    }
}

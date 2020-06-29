using AM_CustomerManager_Data_EFC.Repositories;
using AM_SupplierManager_Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_SupplierManager_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddScoped<ISupplierNoteRepository>(serviceProvider => new SupplierNoteRepository(connectionString));
            services.AddScoped<ISupplierRepository>(serviceProvider => new SupplierRepository(connectionString));
            services.AddScoped<IAttachmentRepository>(serviceProvider => new AttachmentRepository(connectionString));

        }
    }
}

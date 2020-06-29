using AM_CustomerManager_Data_EFC.Repositories;
using AM_ExpensesManager_Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_ExpensesManager_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddScoped<IExpenseLineRepository>(serviceProvider => new ExpenseLineRepository(connectionString));
            services.AddScoped<IExpenseRepository>(serviceProvider => new ExpenseRepository(connectionString));

        }
    }
}

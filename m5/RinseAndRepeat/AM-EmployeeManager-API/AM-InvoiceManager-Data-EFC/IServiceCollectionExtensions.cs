using AM_CustomerManager_Data_EFC.Repositories;
using AM_EmployeeManager_Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AM_EmployeeManager_Data_Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDapperDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddScoped<IEquipmentRepository>(serviceProvider => new EquipmentRepository(connectionString));
            services.AddScoped<IAbsenceRepository>(serviceProvider => new AbsenceRepository(connectionString));
            services.AddScoped<IHolidayRepository>(serviceProvider => new HolidayRepository(connectionString));
            services.AddScoped<IPayRepository>(serviceProvider => new PayRepository(connectionString));
            services.AddScoped<ITaxInformationRepository>(serviceProvider => new TaxInformationRepository(connectionString));
            services.AddScoped<IEmployeeRepository>(serviceProvider => new EmployeeRepository(connectionString));
            services.AddScoped<ITrainingRepository>(serviceProvider => new TrainingRepository(connectionString));
        }
    }
}

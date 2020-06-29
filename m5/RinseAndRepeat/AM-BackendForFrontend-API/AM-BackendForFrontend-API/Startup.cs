using AM_BackendForFrontend_Data.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using AM_BackendForFrontend_Data.Data;

namespace AM_BackendForFrontend_API
{
    public class Startup
    {

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBaseAddress, BaseAddressConfig>();

            services.AddSingleton<ICustomerManagerBaseAddress>(a => new CustomerManagerBaseAddress(Configuration.GetValue<string>("CustomerManagerBaseAddress")));
            services.AddSingleton(typeof(ICustomerManagerRepository<>), typeof(CustomerManagerRepository<>));

            services.AddSingleton<IAccountManagerBaseAddress>(a => new AccountManagerBaseAddress(Configuration.GetValue<string>("AccountManagerBaseAddress")));
            services.AddSingleton(typeof(IAccountManagerRepository<>), typeof(AccountManagerRepository<>));

            services.AddSingleton<IInvoiceManagerBaseAddress>(a => new InvoiceManagerBaseAddress(Configuration.GetValue<string>("InvoiceManagerBaseAddress")));
            services.AddSingleton(typeof(IInvoiceManagerRepository<>), typeof(InvoiceManagerRepository<>));

            services.AddSingleton<IEmployeeManagerBaseAddress>(a => new EmployeeManagerBaseAddress(Configuration.GetValue<string>("EmployeeManagerBaseAddress")));
            services.AddSingleton(typeof(IEmployeeManagerRepository<>), typeof(EmployeeManagerRepository<>));

            services.AddSingleton<IExpenseManagerBaseAddress>(a => new ExpenseManagerBaseAddress(Configuration.GetValue<string>("ExpenseManagerBaseAddress")));
            services.AddSingleton(typeof(IExpenseManagerRepository<>), typeof(ExpenseManagerRepository<>));

            services.AddSingleton<ISupplierManagerBaseAddress>(a => new SupplierManagerBaseAddress(Configuration.GetValue<string>("SupplierManagerBaseAddress")));
            services.AddSingleton(typeof(ISupplierManagerRepository<>), typeof(SupplierManagerRepository<>));

            services.AddSingleton<INotificationsServiceBaseAddress>(a => new NotificationsServiceBaseAddress(Configuration.GetValue<string>("NotificationsServiceBaseAddress")));
            services.AddSingleton(typeof(INotificationsServiceRepository<>), typeof(NotificationsServiceRepository<>));


            services.RegisterSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

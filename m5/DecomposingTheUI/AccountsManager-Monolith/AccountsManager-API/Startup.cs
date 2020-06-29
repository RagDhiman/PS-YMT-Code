using AccountsManager_Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using AccountsManager_Domain.RemoteDataRepositories;
using AccountsManager_Data.Repositories;

namespace AccountsManager_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                 });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.RegisterDataAccess(Configuration.GetConnectionString("AccountManagerConnection"));
            services.RegisterServices();
            services.RegisterSwaggerForAccountManager();

            var useCustomerManagerAPI = Configuration.GetValue<bool>("UseCustomerManagerAPI");

            if (useCustomerManagerAPI)
            {
                services.AddSingleton<IBankAccountRepository, BankAccountRepositoryService>();
                services.AddSingleton<ICustomerRepository, CustomerRepositoryService>();
            }
            else
            {
                services.AddScoped<IBankAccountRepository, BankAccountRepository>();
                services.AddScoped<ICustomerRepository, CustomerRepository>();
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerForAccountManager();

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

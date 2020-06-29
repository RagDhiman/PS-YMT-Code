using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace AM_SupplierManager_API
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwaggerForCustomerManager(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v55", new OpenApiInfo
                {
                    Version = "v55.5",
                    Title = "AM Supplier Manager API",
                    Description = "AM Supplier Manager API for Account Manager Web and Account Manager Mobile"
                });
            });
        }

        public static void UseSwaggerForCustomerManager(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v55/swagger.json", "v55.5");
            });
        }
    }
}

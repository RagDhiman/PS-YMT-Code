using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AM_BackendForFrontend_API
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v55", new OpenApiInfo
                {
                    Version = "v55.5",
                    Title = "AM Backend For Frontend API",
                    Description = "AM Backend For Frontend API for Account Manager Web and Account Manager Mobile"
                });
            });
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v55/swagger.json", "v55.5");
            });
        }
    }
}

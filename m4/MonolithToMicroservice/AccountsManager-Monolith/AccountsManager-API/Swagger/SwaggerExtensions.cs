using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AccountsManager_API
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwaggerForAccountManager(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v55", new OpenApiInfo
                {
                    Version = "v55.5",
                    Title = "Account Manager API",
                    Description = "Account Manager API for Account Manager Web and Account Manager Mobile"
                });
            });
        }

        public static void UseSwaggerForAccountManager(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v55/swagger.json", "v55.5");
            });
        }
    }
}

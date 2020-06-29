using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsManager_WebUI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountsManager_WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //replaces the old .AddMVC() !!
            services.AddScoped<ICustomerRepository, MockCustomerRepository>();

            //services.AddHttpContextAccessor(); //Httpcontext object is used to access sessions data: required to access from non controller class
            //services.AddSession(); //if u want to use sessions, cookies contain sessing ID passed in with every reque
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //Nice error page in development mode
            }

            app.UseHttpsRedirection(); //Force use of https
            app.UseStatusCodePages(); //Add support for text only headers for common status codes
            app.UseStaticFiles(); //allows you to server static files

            //app.UseSession(); //if u want to use sessions, cookies contain sessing ID passed in with every request
            //always call before use routing

            app.UseRouting();

            //enables routing to the correct endpoint
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                //instead map an inomcing request to an action and controller:
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=List}/{id:int?}"
                );

            });
        }
    }
}

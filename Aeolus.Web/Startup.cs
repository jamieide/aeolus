using System;
using Aeolus.Web.AeolusApiService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Aeolus.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddHttpClient<IAeolusApiService, AeolusApiService.AeolusApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:56114");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}

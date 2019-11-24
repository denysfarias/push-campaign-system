using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.PushCampaignService;
using WebApi.PushCampaignService.DataStore;
using WebApi.PushCampaignService.Domain;
using WebApi.PushCampaignService.Domain.DataStore;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;
using WebApi.PushNotificationProviders;

namespace WebApi
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
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Push Campaign API", Version = "v1" });
            });

            // Dependecy Injection
            services.AddSingleton(typeof(ILogger<CampaignsController>), typeof(Logger<CampaignsController>));
            services.AddSingleton(typeof(ILogger<VisitsController>), typeof(Logger<VisitsController>));

            services.AddTransient<ICampaignManager, CampaignManager>();
            services.AddTransient<IVisitManager, VisitManager>();
            services.AddTransient<IDataStore<Campaign>, MockCampaignStore>();
            services.AddTransient<IDataStore<Visit>, MockVisitStore>();
            services.AddTransient<ICampaignSearch, SimpleCampaignSearch>();
            services.AddTransient<IPushNotificationProviderFactory, PushNotificationProviderFactory>();
            services.AddTransient<TextWriter>(service => Console.Out);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Push Campaign API v1");
                c.RoutePrefix = string.Empty; // Start at web application root
            });


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

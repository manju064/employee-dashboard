// <copyright file="Startup.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using EmployeeDashboard.Extensions;
using EmployeeDashboard.Infrastructure.Middleware;
using EmployeeDashboard.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using Serilog;

namespace EmployeeDashboard
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataStores(this.Configuration);

            services.AddMvcCustom();

            services.AddSwagger();

            services.AddCustomInstanceRegistrations();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="appLifetime">IApplicationLifetime</param>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime,
            ILoggerFactory loggerFactory)
        {
            if (env.IsProduction())
            {
                app.UseHsts();
            }

            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
            loggerFactory.AddSerilog();

            // Ensure any buffered events are sent at shutdown
            loggerFactory.AddFile(this.Configuration.GetSection("Logging:Serilog"));

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(config =>
            {
                config.SwaggerRoute = "/swagger/v1/swagger.json";
                config.SwaggerUiRoute = "/swagger";

                config.GeneratorSettings.Title = Global.ApiDescription;
                config.GeneratorSettings.Description = Global.ApiDescription;
                config.ValidateSpecification = true;
            });

            // add middleware
            app.UseMiddleware<LogResponseMiddleware>();
            app.UseMiddleware<LogRequestMiddleware>();

            // ErrorWrappingMiddleware should be las one in MW pipeline
            app.UseMiddleware<ErrorWrappingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

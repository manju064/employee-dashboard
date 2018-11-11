// <copyright file="ServiceCollectionExtensions.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using EmployeeDashboard.Infrastructure.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Core;

namespace EmployeeDashboard.Extensions
{
    /// <summary>
    /// IServiceCollection configuration extension
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension to configure data store
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>configured IServiceCollection</returns>
        public static IServiceCollection AddDataStores(this IServiceCollection services, IConfiguration configuration)
        {
            // your database context registration
            return services;
        }

        /// <summary>
        /// Extension to configure MVC options
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>configured IServiceCollection</returns>
        public static IServiceCollection AddMvcCustom(this IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                // now each route on every controller gets an "api/v{apiVersion}" prefix
                opt.UseGlobalRoutePrefix(new RouteAttribute("api"));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });

            // Add MVC Core
            services
                .AddMvcCore(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                });

            return services;
        }

        /// <summary>
        /// Extension to configure application classes instance registrayions
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>configured IServiceCollection</returns>
        public static IServiceCollection AddCustomInstanceRegistrations(this IServiceCollection services)
        {
            // Your custom service class instances registrations
            return services;
        }
    }
}

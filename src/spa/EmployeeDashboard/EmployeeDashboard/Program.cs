// <copyright file="Program.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeDashboard
{
    /// <summary>
    /// Application start class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry
        /// </summary>
        /// <param name="args">arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create the web host builder
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

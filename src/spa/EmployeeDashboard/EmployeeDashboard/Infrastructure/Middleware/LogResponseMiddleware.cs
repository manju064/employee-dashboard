// <copyright file="LogResponseMiddleware.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.IO;
using System.Threading.Tasks;
using EmployeeDashboard.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmployeeDashboard.Infrastructure.Middleware
{
    /// <summary>
    /// Log Response
    /// Credits: https://github.com/sulhome/log-request-response-middleware
    /// </summary>
    public class LogResponseMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogResponseMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="logger">ILogger</param>
        public LogResponseMiddleware(RequestDelegate next, ILogger<LogResponseMiddleware> logger)
            : base(next, logger)
        {
        }

        /// <summary>
        /// Invoke Log Response Middleware
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>next Task</returns>
        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            if (!bodyStream.CanRead || !bodyStream.CanWrite)
            {
                await this.next(context);
                return;
            }

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await this.next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            this.logger.Log(LogLevel.Information, Global.LogResponseEventId, $"RESPONSE LOG: {responseBody}", null, this.defaultFormatter);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }
}

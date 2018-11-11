// <copyright file="LogRequestMiddleware.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.IO;
using System.Threading.Tasks;
using EmployeeDashboard.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace EmployeeDashboard.Infrastructure.Middleware
{
    /// <summary>
    /// Log request
    /// Credits: https://github.com/sulhome/log-request-response-middleware
    /// </summary>
    public class LogRequestMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogRequestMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="logger">ILogger</param>
        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
            : base(next, logger)
        {
        }

        /// <summary>
        /// Invoke Log Request Middleware
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>next Task</returns>
        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = context.Request.GetDisplayUrl();
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            this.logger.Log(LogLevel.Information, Global.LogRequestEventId, $"REQUEST METHOD: {context.Request.Method}, REQUEST BODY: {requestBodyText}, REQUEST URL: {url}", null, this.defaultFormatter);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await this.next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}

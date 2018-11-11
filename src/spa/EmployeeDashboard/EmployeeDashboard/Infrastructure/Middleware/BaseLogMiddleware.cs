// <copyright file="BaseLogMiddleware.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmployeeDashboard.Infrastructure.Middleware
{
    /// <summary>
    /// Base class for logger middleware
    /// </summary>
    public abstract class BaseLogMiddleware
    {
        /// <summary>
        /// Next processing pipeline
        /// </summary>
        protected readonly RequestDelegate next;

        /// <summary>
        /// logger
        /// </summary>
        protected readonly ILogger<BaseLogMiddleware> logger;

        /// <summary>
        /// default formatter for logs
        /// </summary>
        protected readonly Func<string, Exception, string> defaultFormatter = (state, exception) => $"{state} - {exception?.Message}";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="logger">ILogger</param>
        protected BaseLogMiddleware(RequestDelegate next, ILogger<BaseLogMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
    }
}

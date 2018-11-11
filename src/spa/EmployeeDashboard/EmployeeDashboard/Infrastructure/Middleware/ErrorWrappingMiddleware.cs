// <copyright file="ErrorWrappingMiddleware.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using EmployeeDashboard.Models.Responses;
using EmployeeDashboard.Models.Responses.Exceptions;
using EmployeeDashboard.Resource;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmployeeDashboard.Infrastructure.Middleware
{
    /// <summary>
    /// Error handling middleware
    /// </summary>
    public class ErrorWrappingMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// environment
        /// </summary>
        private readonly IHostingEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorWrappingMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="logger">ILogger</param>
        /// <param name="env">IHostingEnvironment</param>
        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger, IHostingEnvironment env)
            : base(next, logger)
        {
            this.env = env;
        }

        /// <summary>
        /// Invoke MW action
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Task for next MW pipeline</returns>
        public async Task Invoke(HttpContext context)
        {
            Exception exception = null;
            string jsonResponse = string.Empty;
            try
            {
                await this.next.Invoke(context);
            }
            catch (BadRequestException badReqEx)
            {
                HandleException(badReqEx, badReqEx.StatusCode, badReqEx.Message);
            }
            catch (NotFoundException notFoundEx)
            {
                HandleException(notFoundEx, notFoundEx.StatusCode, notFoundEx.Message);
            }
            catch (UnauthorizedException authEx)
            {
                HandleException(authEx, authEx.StatusCode, authEx.Message);
            }
            catch (ApiException apiEx)
            {
                HandleException(apiEx, apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                HandleException(ex, HttpStatusCode.InternalServerError, Global.InternalServerError);
            }

            void HandleException(Exception ex, HttpStatusCode httpStatusCode, string message)
            {
                exception = ex;
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";

                var instanceId = $"urn:employee-dashboard:error:{Guid.NewGuid()}";
                var eventId = new EventId((int)httpStatusCode, instanceId);

                this.logger.LogError(eventId, ex.Demystify(), ex.Message);

                // Log stack trace
                ApiResponse response = !this.env.IsProduction() ? new JsonErrorResponse(instanceId, httpStatusCode, message, ex.Demystify().ToString())
                                                                : new JsonErrorResponse(instanceId, httpStatusCode, message);

                jsonResponse = JsonConvert.SerializeObject(
                    response,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }

            if (!context.Response.HasStarted && exception != null)
            {
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        /// <summary>
        /// Error response for exceptions
        /// </summary>
        private class JsonErrorResponse : ApiResponse
        {
            public JsonErrorResponse(string instance, HttpStatusCode statusCode, string message = null, string developerMessage = null)
                : base(statusCode, message)
            {
                this.DeveloperMessage = developerMessage;
                this.Instance = instance;
            }

            /// <summary>
            /// Gets or sets detail stack trace
            /// </summary>
            private string DeveloperMessage { get; set; }

            /// <summary>
            /// Gets or sets a URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced.
            /// </summary>
            private string Instance { get; set; }
        }
    }
}

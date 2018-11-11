// <copyright file="ApiResponse.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Net;
using EmployeeDashboard.Resource;
using Newtonsoft.Json;

namespace EmployeeDashboard.Models.Responses
{
    /// <summary>
    /// Api Response base class
    /// Creedits: https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <param name="message">string</param>
        public ApiResponse(HttpStatusCode statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? this.GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Gets status code of the response
        /// TODO, implement custom status rather than standard http status
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets Message
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        /// <summary>
        /// Get Default message for statusCode
        /// </summary>
        /// <param name="status">HttpStatusCode</param>
        /// <returns>string</returns>
        private string GetDefaultMessageForStatusCode(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.OK:
                    return Global.SuccessMessage;
                case HttpStatusCode.NotFound:
                    return Global.NotFound;
                case HttpStatusCode.BadRequest:
                    return Global.BadRequest;
                case HttpStatusCode.InternalServerError:
                    return Global.InternalServerError;
                case HttpStatusCode.Unauthorized:
                    return Global.Unauthorized;
                default:
                    return null;
            }
        }
    }
}

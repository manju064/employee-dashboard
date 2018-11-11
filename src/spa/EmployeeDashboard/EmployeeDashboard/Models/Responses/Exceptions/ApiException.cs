// <copyright file="ApiException.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System;
using System.Net;
using System.Runtime.Serialization;

namespace EmployeeDashboard.Models.Responses.Exceptions
{
    /// <summary>
    /// ApiException object to manage exceptions for api
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <param name="errorCode">errorcode</param>
        /// <param name="errorDescription">errorDescription</param>
        public ApiException(HttpStatusCode statusCode, string errorCode, string errorDescription)
            : base($"{errorDescription}")
        {
            this.StatusCode = statusCode;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode</param>
        public ApiException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Gets status code of the exception
        /// /// TODO, implement custom status rather than standard http status
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets get ErrorCode(for internal purpose)
        /// </summary>
        public string ErrorCode { get; }
    }
}

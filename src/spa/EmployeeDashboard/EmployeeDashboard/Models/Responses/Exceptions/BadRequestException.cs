// <copyright file="BadRequestException.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Net;

namespace EmployeeDashboard.Models.Responses.Exceptions
{
    /// <summary>
    /// BadRequestException for 400 status
    /// </summary>
    public class BadRequestException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        public BadRequestException()
            : base(HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">string</param>
        /// <param name="errorCode">errorCode</param>
        public BadRequestException(string message, string errorCode = "400")
            : base(HttpStatusCode.BadRequest, errorCode, message)
        {
        }
    }
}

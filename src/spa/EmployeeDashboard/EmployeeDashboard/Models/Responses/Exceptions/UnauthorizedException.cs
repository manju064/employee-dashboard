// <copyright file="UnauthorizedException.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Net;

namespace EmployeeDashboard.Models.Responses.Exceptions
{
    /// <summary>
    /// UnauthorizedException for 401
    /// </summary>
    public class UnauthorizedException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException()
            : base(HttpStatusCode.Unauthorized)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">string</param>
        public UnauthorizedException(string message)
            : base(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), message)
        {
        }
    }
}

// <copyright file="ApiOkResponse.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Net;
using EmployeeDashboard.Resource;

namespace EmployeeDashboard.Models.Responses
{
    /// <summary>
    /// Object for sending sucessfull message
    /// </summary>
    /// <typeparam name="T">data</typeparam>
    public class ApiOkResponse<T> : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiOkResponse{T}"/> class.
        /// </summary>
        /// <param name="data">Generica type data</param>
        public ApiOkResponse(T data)
            : base(HttpStatusCode.OK, Global.SuccessMessage)
        {
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiOkResponse{T}"/> class with HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <param name="data">Generica type data</param>
        public ApiOkResponse(HttpStatusCode statusCode, T data)
            : base(statusCode, Global.SuccessMessage)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets Data
        /// </summary>
        public T Data { get; }
    }
}

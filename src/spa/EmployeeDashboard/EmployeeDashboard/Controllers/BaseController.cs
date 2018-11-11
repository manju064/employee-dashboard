// <copyright file="BaseController.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeDashboard.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="configuration">configuration</param>
        public BaseController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration object
        /// </summary>
        protected IConfiguration Configuration { get; }
    }
}

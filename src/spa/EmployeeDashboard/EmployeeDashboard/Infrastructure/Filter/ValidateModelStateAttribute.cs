// <copyright file="ValidateModelStateAttribute.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeDashboard.Infrastructure.Filter
{
    /// <summary>
    /// Global filter to check model state
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}

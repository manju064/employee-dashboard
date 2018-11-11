// <copyright file="Error.cshtml.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeDashboard.Pages
{
    /// <summary>
    /// Error page Model
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets RequestId
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the request id
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        /// <summary>
        /// Return error id
        /// </summary>
        public void OnGet() => this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
    }
}

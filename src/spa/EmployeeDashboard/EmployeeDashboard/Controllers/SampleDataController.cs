// <copyright file="SampleDataController.cs" company="Manjunath Keshava">
// Manjunath Keshava Copyright (c) 2018
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeDashboard.Controllers
{
    /// <summary>
    /// Sample data controller
    /// </summary>
    [Route("[controller]")]
    public class SampleDataController : BaseController
    {
        /// <summary>
        /// summries text
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataController"/> class.
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public SampleDataController(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary>
        /// Get weatherforecasts
        /// </summary>
        /// <returns>list of weatherforecasts objects</returns>
        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        /// <summary>
        /// Weather Forecast
        /// </summary>
        public class WeatherForecast
        {
            /// <summary>
            /// Gets or sets date Formatted
            /// </summary>
            public string DateFormatted { get; set; }

            /// <summary>
            /// Gets or sets temperatureC
            /// </summary>
            public int TemperatureC { get; set; }

            /// <summary>
            /// Gets or sets Summary
            /// </summary>
            public string Summary { get; set; }

            /// <summary>
            /// Gets TemperatureF
            /// </summary>
            public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
        }
    }
}

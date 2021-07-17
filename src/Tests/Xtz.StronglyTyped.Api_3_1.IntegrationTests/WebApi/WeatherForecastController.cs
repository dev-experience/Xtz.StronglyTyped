using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.Api_3_1.IntegrationTests.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] SUMMARIES = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = SUMMARIES[rng.Next(SUMMARIES.Length)]
            })
            .ToArray();
        }

        [HttpGet("strongly-typed")]
        public IEnumerable<StronglyTypedWeatherForecast> StronglyTypedGet()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5)
                .Select(index => new StronglyTypedWeatherForecast
                {
                    City = new City("Amsterdam"),
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = (DegreesCelsius)rng.Next(-20, 55),
                    Summary = SUMMARIES[rng.Next(SUMMARIES.Length)]
                })
                .ToArray();
        }
    }
}

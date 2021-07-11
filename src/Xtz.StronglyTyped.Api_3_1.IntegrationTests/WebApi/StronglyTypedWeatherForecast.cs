using System;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.Api_3_1.IntegrationTests.WebApi
{
    public class StronglyTypedWeatherForecast
    {
        public City City { get; set; }

        public DateTime Date { get; set; }

        public DegreesCelsius TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

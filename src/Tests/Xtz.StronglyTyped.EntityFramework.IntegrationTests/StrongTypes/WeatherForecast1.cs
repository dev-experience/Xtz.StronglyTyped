using System;
using System.ComponentModel.DataAnnotations;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.Finance;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    public class WeatherForecast1
    {
        [Key]
        public WeatherForecastGuidId GuidId { get; set; }

        public City City { get; set; }

        public Email Email { get; set; }

        public DateTime Date { get; set; }

        public Amount Amount { get; set; }
    }
}
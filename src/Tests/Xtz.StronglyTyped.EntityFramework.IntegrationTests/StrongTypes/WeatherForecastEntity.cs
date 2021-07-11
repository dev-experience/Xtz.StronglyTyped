using System;
using System.ComponentModel.DataAnnotations;
using Xtz.StronglyTyped.BuiltinTypes.Finance;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    public class WeatherForecastEntity
    {
        [Key]
        public WeatherForecastGuidId Id { get; set; }

        public CityIntId CityId { get; set; }

        public CityEntity City { get; set; }

        public Email Email { get; set; }

        public DateTime Date { get; set; }

        public Amount Amount { get; set; }
    }
}
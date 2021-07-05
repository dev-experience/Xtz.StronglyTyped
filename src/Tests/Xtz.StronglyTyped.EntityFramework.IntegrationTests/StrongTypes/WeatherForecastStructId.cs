using System;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    [StrongType(typeof(Guid))]
    public partial struct WeatherForecastStructId
    {
        public static WeatherForecastStructId New()
        {
            return new WeatherForecastStructId(Guid.NewGuid());
        }
    }
}
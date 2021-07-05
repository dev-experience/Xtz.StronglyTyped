using System;
using Xtz.StronglyTyped.BuiltinTypes.Ids;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    [StrongType(typeof(Guid))]
    public partial class WeatherForecastGuidId : GuidId
    {
    }
}
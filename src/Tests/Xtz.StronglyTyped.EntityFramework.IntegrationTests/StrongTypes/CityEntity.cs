using System.ComponentModel.DataAnnotations;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    public class CityEntity
    {
        [Key]
        public CityIntId Id { get; set; }

        public City Name { get; set; }
    }
}
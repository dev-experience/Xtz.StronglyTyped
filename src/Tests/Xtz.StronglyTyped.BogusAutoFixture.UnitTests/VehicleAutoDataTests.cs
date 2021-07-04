using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Vehicle;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class VehicleAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            FuelType fuelType,
            VehicleManufacturer vehicleManufacturer,
            VehicleModel vehicleModel,
            VehicleType vehicleType,
            Vin vin)
        {
            var values = new object[]
            {
                fuelType,
                vehicleManufacturer,
                vehicleModel,
                vehicleType,
                vin,
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}
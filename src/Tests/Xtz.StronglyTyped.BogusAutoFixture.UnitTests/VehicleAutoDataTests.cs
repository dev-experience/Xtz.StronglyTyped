using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
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

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString().IsBogusGeneratedValue()));
        }
    }
}
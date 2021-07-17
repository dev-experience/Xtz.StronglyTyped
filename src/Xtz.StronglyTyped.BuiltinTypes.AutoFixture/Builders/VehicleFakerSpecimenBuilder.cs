using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Vehicle;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class VehicleFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly VehicleFakerBuilder _builder = new();

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(FuelType), () => _builder.BuildFuelTypeFaker() },
            { typeof(VehicleManufacturer), () => _builder.BuildVehicleManufacturerFaker() },
            { typeof(VehicleModel), () => _builder.BuildVehicleModelFaker() },
            { typeof(VehicleType), () => _builder.BuildVehicleTypeFaker() },
            { typeof(Vin), () => _builder.BuildVinFaker() },
        };
    }
}
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Vehicle;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class VehicleFakerBuilder : BaseFakerBuilder
    {
        /// <param name="useFakerCache">Do store created faker objects in in-memory cache</param>
        public VehicleFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random vehicle fuel type faker.
        /// </summary>
        /// <remarks>Example: Electric, Gasoline, Diesel.</remarks>
        public Faker<FuelType> BuildFuelTypeFaker()
        {
            var result = GetFaker(() => new Faker<FuelType>()
                .CustomInstantiator(f => new FuelType(f.Vehicle.Fuel())));
            return result;
        }

        /// <summary>
        /// A random vehicle manufacture name faker.
        /// </summary>
        /// <remarks>Example: Toyota, Ford, Porsche</remarks>
        public Faker<VehicleManufacturer> BuildVehicleManufacturerFaker()
        {
            var result = GetFaker(() => new Faker<VehicleManufacturer>()
                .CustomInstantiator(f => new VehicleManufacturer(f.Vehicle.Manufacturer())));
            return result;
        }

        /// <summary>
        /// A random vehicle model faker.
        /// </summary>
        /// <remarks>Example: Camry, Civic, Accord</remarks>
        public Faker<VehicleModel> BuildVehicleModelFaker()
        {
            var result = GetFaker(() => new Faker<VehicleModel>()
                .CustomInstantiator(f => new VehicleModel(f.Vehicle.Model())));
            return result;
        }

        /// <summary>
        /// A random vehicle type faker.
        /// </summary>
        /// <remarks>Example: Minivan, SUV, Sedan.</remarks>
        public Faker<VehicleType> BuildVehicleTypeFaker()
        {
            var result = GetFaker(() => new Faker<VehicleType>()
                .CustomInstantiator(f => new VehicleType(f.Vehicle.Type())));
            return result;
        }

        /// <summary>
        /// A random vehicle identification number (VIN) faker.
        /// </summary>
        public Faker<Vin> BuildVinFaker()
        {
            var result = GetFaker(() => new Faker<Vin>()
                .CustomInstantiator(f => new Vin(f.Vehicle.Vin())));
            return result;
        }
    }
}
using Bogus;
using Bogus.DataSets;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class AddressFakerBuilder : BaseFakerBuilder
    {
        public AddressFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random city name faker.
        /// </summary>
        public Faker<City> BuildCityFaker()
        {
            var result = GetFaker(() => new Faker<City>()
                .CustomInstantiator(f => new City(f.Address.City())));
            return result;
        }

        /// <summary>
        /// A random city prefix faker.
        /// </summary>
        /// <remarks>Example: "North", "Port", "New", etc. </remarks>
        public Faker<CityPrefix> BuildCityPrefixFaker()
        {
            var result = GetFaker(() => new Faker<CityPrefix>()
                .CustomInstantiator(f => new CityPrefix(f.Address.CityPrefix())));
            return result;
        }

        /// <summary>
        /// A random city suffix faker.
        /// </summary>
        /// <remarks>Example: "ville", "side", "stad", "port", "chester", etc. </remarks>
        public Faker<CitySuffix> BuildCitySuffixFaker()
        {
            var result = GetFaker(() => new Faker<CitySuffix>()
                .CustomInstantiator(f => new CitySuffix(f.Address.CitySuffix())));
            return result;
        }

        /// <summary>
        /// A random country faker.
        /// </summary>
        public Faker<Country> BuildCountryFaker()
        {
            var result = GetFaker(() => new Faker<Country>()
                .CustomInstantiator(f => new Country(f.Address.Country())));
            return result;
        }

        /// <summary>
        /// A random ISO 3166-1 country code faker.
        /// </summary>
        /// <param name="format">The format the country code should be in.</param>
        public Faker<CountryCode> BuildCountryCodeFaker(Iso3166Format format = Iso3166Format.Alpha2)
        {
            var result = GetFaker(() => new Faker<CountryCode>()
                .CustomInstantiator(f => new CountryCode(f.Address.CountryCode(format))), format.ToString());
            return result;
        }

        /// <summary>
        /// A random county faker.
        /// </summary>
        public Faker<County> BuildCountyFaker()
        {
            var result = GetFaker(() => new Faker<County>()
                .CustomInstantiator(f => new County(f.Address.County())));
            return result;
        }

        /// <summary>
        /// A random full address (Street, City, Country) faker.
        /// </summary>
        public Faker<FullAddress> BuildFullAddressFaker()
        {
            var result = GetFaker(() => new Faker<FullAddress>()
                .CustomInstantiator(f => new FullAddress(f.Address.FullAddress())));
            return result;
        }

        /// <summary>
        /// A random secondary address (e.g. 'Apt. 2' or 'Suite 321') faker.
        /// </summary>
        public Faker<SecondaryAddress> BuildSecondaryAddressFaker()
        {
            var result = GetFaker(() => new Faker<SecondaryAddress>()
                .CustomInstantiator(f => new SecondaryAddress(f.Address.SecondaryAddress())));
            return result;
        }

        /// <summary>
        /// A random state faker.
        /// </summary>
        public Faker<State> BuildStateFaker()
        {
            var result = GetFaker(() => new Faker<State>()
                .CustomInstantiator(f => new State(f.Address.State())));
            return result;
        }

        /// <summary>
        /// An abbreviation for a random state faker.
        /// </summary>
        public Faker<StateAbbr> BuildStateAbbrFaker()
        {
            var result = GetFaker(() => new Faker<StateAbbr>()
                .CustomInstantiator(f => new StateAbbr(f.Address.StateAbbr())));
            return result;
        }

        /// <summary>
        /// A random street address faker.
        /// </summary>
        /// <param name="useFullAddress">If true, the returned value will also include a secondary address.</param>
        public Faker<StreetAddress> BuildStreetAddressFaker(bool useFullAddress = false)
        {
            var result = GetFaker(() => new Faker<StreetAddress>()
                .CustomInstantiator(f => new StreetAddress(f.Address.StreetAddress(useFullAddress))), useFullAddress.ToString());
            return result;
        }

        /// <summary>
        /// A random street name faker.
        /// </summary>
        public Faker<StreetName> BuildStreetNameFaker()
        {
            var result = GetFaker(() => new Faker<StreetName>()
                .CustomInstantiator(f => new StreetName(f.Address.StreetName())));
            return result;
        }

        /// <summary>
        /// A random street suffix faker.
        /// </summary>
        public Faker<StreetSuffix> BuildStreetSuffixFaker()
        {
            var result = GetFaker(() => new Faker<StreetSuffix>()
                .CustomInstantiator(f => new StreetSuffix(f.Address.StreetSuffix())));
            return result;
        }

        /// <summary>
        /// A random postal code faker.
        /// </summary>
        /// <param name="format">
        /// If a format is provided it will fill the format with letters and numbers.
        /// Example "???? ##" can become "QYTE 78".
        /// </param>
        public Faker<PostalCode> BuildPostalCodeFaker(string? format = null)
        {
            var result = GetFaker(() => new Faker<PostalCode>()
                .CustomInstantiator(f => new PostalCode(f.Address.ZipCode(format))), format);
            return result;
        }
    }
}
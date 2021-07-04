using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    // TODO: Add integration tests to check whether all types are included to the builders
    public class AddressFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly AddressFakerBuilder _builder = new(true);

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(City), () => _builder.BuildCityFaker() },
            { typeof(CityPrefix), () => _builder.BuildCityPrefixFaker() },
            { typeof(CitySuffix), () => _builder.BuildCitySuffixFaker() },
            { typeof(Country), () => _builder.BuildCountryFaker() },
            { typeof(CountryCode), () => _builder.BuildCountryCodeFaker() },
            { typeof(County), () => _builder.BuildCountyFaker() },
            { typeof(FullAddress), () => _builder.BuildFullAddressFaker() },
            { typeof(SecondaryAddress), () => _builder.BuildSecondaryAddressFaker() },
            { typeof(State), () => _builder.BuildStateFaker() },
            { typeof(StateAbbr), () => _builder.BuildStateAbbrFaker() },
            { typeof(StreetAddress), () => _builder.BuildStreetAddressFaker() },
            { typeof(StreetName), () => _builder.BuildStreetNameFaker() },
            { typeof(StreetSuffix), () => _builder.BuildStreetSuffixFaker() },
            { typeof(PostalCode), () => _builder.BuildPostalCodeFaker() },
        };
    }
}
using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class AddressAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            City city,
            CityPrefix cityPrefix,
            CitySuffix citySuffix,
            Country country,
            CountryCode countryCode,
            County county,
            FullAddress fullAddress,
            SecondaryAddress secondaryAddress,
            State state,
            StateAbbr stateAbbr,
            StreetAddress streetAddress,
            StreetName streetName,
            StreetSuffix streetSuffix,
            PostalCode postalCode)
        {
            var values = new object[]
            {
                city,
                cityPrefix,
                citySuffix,
                country,
                countryCode,
                county,
                fullAddress,
                secondaryAddress,
                state,
                stateAbbr,
                streetAddress,
                streetName,
                streetSuffix,
                postalCode,
            };

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }
    }
}
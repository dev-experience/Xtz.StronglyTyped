using NUnit.Framework;
using System;
using System.Globalization;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class ConversionTests
    {
        [TestCase("Norway")]
        [Test]
        public void ShouldParseStronglyTyped_GivenString_WhenExplicitlyConverted(string country)
        {
            //// Arrange

            var expected = new Country(country);

            //// Act

            var result = (Country)country;

            //// Assert

            Assert.AreEqual(expected, result);
        }

        [TestCase("Norway")]
        [Test]
        public void ShouldConvertToString_GivenStronglyTyped_WhenExplicitlyConverted(string country)
        {
            //// Arrange

            var stronglyTyped = new Country(country);

            //// Act

            var result = (string)stronglyTyped;

            //// Assert

            Assert.AreEqual(country, result);
        }

        [TestCase("Norway")]
        [Test]
        public void ShouldConvertToString_GivenStronglyTyped_WhenImplicitlyConverted(string country)
        {
            //// Arrange

            var stronglyTyped = new Country(country);

            //// Act

            string result = stronglyTyped;

            //// Assert

            Assert.AreEqual(country, result);
        }

        [TestCase("Norway")]
        [Test]
        public void Convert_ShouldConvertToString_GivenCountry(string country)
        {
            //// Arrange

            var stronglyTyped = new Country(country) as object;

            //// Act

            var result = Convert.ToString(stronglyTyped, CultureInfo.InvariantCulture);

            //// Assert

            Assert.AreEqual(country, result);
        }

        [TestCase(27)]
        [Test]
        public void Convert_ShouldConvertToInt_GivenCountryId(int countryId)
        {
            //// Arrange

            var stronglyTyped = new CountryId(countryId);

            //// Act

            var result = Convert.ToInt32(stronglyTyped, CultureInfo.InvariantCulture);

            //// Assert

            Assert.AreEqual(countryId, result);
        }
    }
}
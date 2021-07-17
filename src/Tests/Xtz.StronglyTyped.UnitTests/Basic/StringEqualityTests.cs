using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class StringEqualityTests
    {
        [TestCase("Norway")]
        [Test]
        public void StronglyTypedValues_ShouldBeEqual_GivenSameStrings(string country)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country);
            var result2 = new Country(country);

            //// Assert

            Assert.That(result2, Is.EqualTo(result1));
            Assert.That(result2.Value, Is.EqualTo(result1.Value));
        }

        [TestCase("Norway", "Denmark")]
        [Test]
        public void StronglyTypedValues_ShouldBeNotEqual_GivenDifferentStrings(string country1, string country2)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country1);
            var result2 = new Country(country2);

            //// Assert

            Assert.That(result2, Is.Not.EqualTo(result1));
            Assert.That(result2.Value, Is.Not.EqualTo(result1.Value));
        }

        [TestCase("Norway")]
        [Test]
        public void ShouldBeEqual_GivenTheSameCalculatedStrings(string country)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country.ToUpper());
            var result2 = new Country(country.ToUpper());

            //// Assert

            Assert.That(result2, Is.EqualTo(result1));
            Assert.That(result2.Value, Is.EqualTo(result1.Value));
        }

        [TestCase("Norway")]
        [Test]
        public void ShouldBeNotEqual_GivenTheDifferentlyCasedValues(string country)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country.ToUpper());
            var result2 = new Country(country);

            //// Assert

            Assert.That(result2, Is.Not.EqualTo(result1));
            Assert.That(result2.Value.ToUpper(), Is.EqualTo(result1.Value));
        }

        [TestCase("Norway")]
        [Test]
        public void GetHashCode_ShouldBeEqual_GivenSameStrings(string country)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country);
            var result2 = new Country(country);

            //// Assert

            Assert.That(result2.GetHashCode(), Is.EqualTo(result1.GetHashCode()));
            Assert.That(result2.GetHashCode(), Is.EqualTo(result1.GetHashCode()));
        }

        [TestCase("Norway", "Denmark")]
        [Test]
        public void GetHashCode_ShouldBeNotEqual_GivenDifferentStrings(string country1, string country2)
        {
            //// Arrange

            //// Act

            var result1 = new Country(country1);
            var result2 = new Country(country2);

            //// Assert

            Assert.That(result2.GetHashCode(), Is.Not.EqualTo(result1.GetHashCode()));
        }
    }
}
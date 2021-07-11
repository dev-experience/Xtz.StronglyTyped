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

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result1.Value, result2.Value);
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

            Assert.AreNotEqual(result1, result2);
            Assert.AreNotEqual(result1.Value, result2.Value);
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

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result1.Value, result2.Value);
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

            Assert.AreNotEqual(result1, result2);
            Assert.AreEqual(result1.Value, result2.Value.ToUpper());
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

            Assert.AreEqual(result1.GetHashCode(), result2.GetHashCode());
            Assert.AreEqual(result1.GetHashCode(), result2.GetHashCode());
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

            Assert.AreNotEqual(result1.GetHashCode(), result2.GetHashCode());
        }
    }
}
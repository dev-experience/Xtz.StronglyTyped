using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class StringCreationTests
    {
        [TestCase("Norway")]
        [Test]
        public void ShouldInstantiate_GivenString(string country)
        {
            //// Arrange

            //// Act

            var result = new Country(country);

            //// Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(country));
        }

        [TestCase("Norway")]
        [Test]
        public void ShouldReturnInnerValue_WhenToString(string country)
        {
            //// Arrange

            var stronglyTyped = new Country(country);

            //// Act

            var result = stronglyTyped.ToString();

            //// Assert

            Assert.That(result, Is.EqualTo(country));
        }
    }
}
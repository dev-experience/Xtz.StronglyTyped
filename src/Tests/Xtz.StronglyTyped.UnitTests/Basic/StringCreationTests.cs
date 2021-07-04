using NUnit.Framework;

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

            Assert.NotNull(result);
            Assert.AreEqual(country, result.Value);
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

            Assert.AreEqual(country, result);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void ShouldThrow_GivenInvalidString(string country)
        {
            //// Arrange


            //// Act

            void Action() => new Country(country);

            //// Assert

            Assert.Throws<StronglyTypedException>(Action);
        }
    }
}
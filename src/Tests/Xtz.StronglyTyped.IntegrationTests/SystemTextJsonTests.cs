using NUnit.Framework;
using System.Text.Json;
using Xtz.StronglyTyped.IntegrationTests.Settings;

namespace Xtz.StronglyTyped.IntegrationTests
{
    public class SystemTextJsonTests
    {
        [Test]
        public void ShouldSerialize_GivenSettings()
        {
            // Arrange

            var settings = new RequiredTestSettings
            {
                Country = new Country("Brazil"),
            };

            // Act

            var result = JsonSerializer.Serialize(settings);

            // Assert

            Assert.That(() => result.Contains("Brazil"));
        }

        [Test]
        public void ShouldSerialize_GivenNestedSettings()
        {
            // Arrange

            var country = "Brazil";
            var settings = new NestedTestSettings
            {
                Inner = new NestedTestSettings.InnerSettings
                {
                    Country = new Country("Brazil"),
                }
            };

            var expected = $@"{{""Inner"":{{""Country"":""{country}""}}}}";

            // Act

            var result = JsonSerializer.Serialize(settings);

            // Assert

            Assert.That(() => result.Contains(country));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldDeserializeSettings_GivenString()
        {
            // Arrange

            var country = "Canada";
            var serialized = $@"{{""Country"": ""{country}""}}";
            var expected = new Country(country);

            // Act

            var result = JsonSerializer.Deserialize<RequiredTestSettings>(serialized);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Country);
        }

        [Test]
        public void ShouldDeserializeNestedSettings_GivenString()
        {
            // Arrange

            var country = "Canada";
            var serialized = $@"{{""Inner"":{{""Country"":""{country}""}}}}";
            var expected = new Country(country);

            // Act

            var result = JsonSerializer.Deserialize<NestedTestSettings>(serialized);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Inner.Country);
        }
    }
}
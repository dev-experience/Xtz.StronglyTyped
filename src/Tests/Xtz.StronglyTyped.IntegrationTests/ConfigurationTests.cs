using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using Xtz.StronglyTyped.IntegrationTests.Settings;

namespace Xtz.StronglyTyped.IntegrationTests
{

    public class ConfigurationTests
    {
        [Test]
        public void ShouldNotFail_WhenBindingFromAppSettingsJson()
        {
            // Arrange

            var settings = new RequiredTestSettings();

            // Act

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            configuration.Bind("Xtz.Test", settings);

            // Assert
            
            // Shouldn't fail
        }

        [Test]
        public void ShouldBindSection_GivenAppSettingsJson()
        {
            // Arrange

            var settings = new RequiredTestSettings();
            var expectedCountry = new Country("Iceland");

            // Act

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            configuration.Bind("Xtz.Test", settings);

            // Assert

            Assert.NotNull(settings);
            Assert.AreEqual(expectedCountry, settings.Country);
        }

        [Test]
        public void Validator_ShouldNotFail_GivenAppSettingsJson()
        {
            // Arrange

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = new RequiredTestSettings();
            configuration.Bind("Xtz.Test", settings);

            // Act

            Validator.ValidateObject(settings, new ValidationContext(settings));

            // Assert

            // Shouldn't fail
        }

        [TestCase("Test1")]
        [Test]
        public void Validator_ShouldFail_GivenInvalidAppSettingsJson(string sectionName)
        {
            // Arrange

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("invalid-appsettings.json")
                .Build();

            var settings = new RequiredTestSettings();
            configuration.Bind($"Xtz.{sectionName}", settings);

            // Act

            TestDelegate action = () => Validator.ValidateObject(settings, new ValidationContext(settings));

            // Assert

            Assert.Throws<ValidationException>(action);
        }
    }
}
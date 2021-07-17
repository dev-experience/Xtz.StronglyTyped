using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
            Assert.That(settings.Country, Is.Not.Null);
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

            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Country, Is.EqualTo(expectedCountry));
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

            [ExcludeFromCodeCoverage]
            void Action() => Validator.ValidateObject(settings, new ValidationContext(settings));

            // Assert

            Assert.Throws<ValidationException>(Action);
        }
    }
}

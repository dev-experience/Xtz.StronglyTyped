using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Xtz.StronglyTyped.Api_3_1.IntegrationTests.WebApi;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.NewtonsoftJson;

namespace Xtz.StronglyTyped.Api_3_1.IntegrationTests
{
    public sealed class WebApiTests : IDisposable
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private readonly WebApiFactory _factory = new();

        private readonly HttpClient _client;

        public WebApiTests()
        {
            _client = _factory.CreateClient();

            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StronglyTypedNewtonsoftConverter());
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        [Test]
        public async Task ShouldReturn200_ForStandardEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);
        }

        [Test]
        public async Task ShouldDeserialize_ForStandardEndpoint_WhenNewtonsoftUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);

            Country country = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var _ = JsonConvert.SerializeObject(country);

            var responseStr = await response.Content.ReadAsStringAsync();
            var __ = JsonConvert.DeserializeObject<IReadOnlyCollection<WeatherForecast>>(responseStr, _jsonSerializerSettings);
        }

        [Test]
        public async Task ShouldDeserialize_ForStandardEndpoint_WhenSystemTextJsonUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);

            var responseStr = await response.Content.ReadAsStringAsync();
            var _ = System.Text.Json.JsonSerializer.Deserialize<IReadOnlyCollection<WeatherForecast>>(responseStr);
        }

        [Test]
        public async Task ShouldReturn200_ForStronglyTypedEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);
        }

        [Test]
        public async Task ShouldDeserialize_ForStronglyTypedEndpoint_WhenNewtonsoftUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);

            var responseStr = await response.Content.ReadAsStringAsync();
            var _ = JsonConvert.DeserializeObject<IReadOnlyCollection<StronglyTypedWeatherForecast>>(responseStr, _jsonSerializerSettings);
        }

        [Test]
        public async Task ShouldDeserialize_ForStronglyTypedEndpoint_WhenSystemTextJsonUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.Content, Is.Not.Null);

            var responseStr = await response.Content.ReadAsStringAsync();
            var _ = System.Text.Json.JsonSerializer.Deserialize<IReadOnlyCollection<StronglyTypedWeatherForecast>>(responseStr);
        }

        public void Dispose()
        {
            _factory?.Dispose();
        }
    }
}
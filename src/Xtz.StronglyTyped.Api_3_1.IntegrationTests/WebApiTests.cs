using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Xtz.StronglyTyped.Api_3_1.IntegrationTests.WebApi;

namespace Xtz.StronglyTyped.Api_3_1.IntegrationTests
{
    public class WebApiTests : IDisposable
    {
        private readonly WebApiFactory _factory = new();
        
        private readonly HttpClient _client;

        public WebApiTests()
        {
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ShouldReturn200_ForStandardEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);
        }

        [Test]
        public async Task ShouldDeserialize_ForStandardEndpoint_WhenNewtonsoftUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);

            var responseStr = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<IReadOnlyCollection<WeatherForecast>>(responseStr);
        }

        [Test]
        public async Task ShouldDeserialize_ForStandardEndpoint_WhenSystemTextJsonUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);

            var responseStr = await response.Content.ReadAsStringAsync();
            var responseObject = System.Text.Json.JsonSerializer.Deserialize<IReadOnlyCollection<WeatherForecast>>(responseStr);
        }

        [Test]
        public async Task ShouldReturn200_ForStronglyTypedEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);
        }

        [Test]
        public async Task ShouldDeserialize_ForStronglyTypedEndpoint_WhenNewtonsoftUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);

            var responseStr = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<IReadOnlyCollection<StronglyTypedWeatherForecast>>(responseStr);
        }

        [Test]
        public async Task ShouldDeserialize_ForStronglyTypedEndpoint_WhenSystemTextJsonUsed()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast/strongly-typed");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);

            var responseStr = await response.Content.ReadAsStringAsync();
            var responseObject = System.Text.Json.JsonSerializer.Deserialize<IReadOnlyCollection<StronglyTypedWeatherForecast>>(responseStr);
        }

        public void Dispose()
        {
            _factory?.Dispose();
        }
    }
}
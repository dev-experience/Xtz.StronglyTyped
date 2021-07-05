using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.Finance;
using Xtz.StronglyTyped.BuiltinTypes.Internet;
using Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests
{
    public class DbContextTests
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(GetType().Namespace!));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void ShouldSucceed_GetRequiredDbContext()
        {
            // Act

            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
        }

        [Test]
        public void ShouldSucceed_EnsureDatabase()
        {
            // Arrange

            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();

            // Act

            dbContext.Database.EnsureCreated();
        }

        [Test]
        public void ShouldSucceed_SeedingData_ForGuidId()
        {
            // Arrange

            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            var entities = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast1
                {
                    GuidId = new WeatherForecastGuidId(),
                    City = new City("Amsterdam"),
                    Email = new Email("bob@example.com"),
                    Date = DateTime.Now.AddDays(index),
                    Amount = (Amount)55,
                })
                .ToArray();

            // Act

            dbContext.WeatherForecasts1.AddRange(entities);
            dbContext.SaveChanges();

        }

        [Test]
        public void ShouldSucceed_SeedingData_ForIntId()
        {
            // Arrange

            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            var entities = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast2
                {
                    IntId = new WeatherForecastIntId(index),
                    City = new City("Amsterdam"),
                    Email = new Email("bob@example.com"),
                    Date = DateTime.Now.AddDays(index),
                    Amount = (Amount)55,
                })
                .ToArray();

            // Act

            dbContext.WeatherForecasts2.AddRange(entities);
            dbContext.SaveChanges();

        }

        [Test]
        public void ShouldSucceed_SeedingData_ForStructId()
        {
            // Arrange

            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            var entities = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast3
                {
                    StructId = WeatherForecastStructId.New(),
                    City = new City("Amsterdam"),
                    Email = new Email("bob@example.com"),
                    Date = DateTime.Now.AddDays(index),
                    Amount = (Amount)55,
                })
                .ToArray();

            // Act

            dbContext.WeatherForecasts3.AddRange(entities);
            dbContext.SaveChanges();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Finance;
using Xtz.StronglyTyped.BuiltinTypes.Internet;
using Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests
{
    public class DbContextTests
    {
        private readonly AppDbContext _dbContext;

        public DbContextTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(GetType().Namespace!));

            var serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        }

        [SetUp]
        public void Setup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Test]
        [StrongAutoData]
        public void ShouldSucceed_SeedingData_ForGuidId(IReadOnlyCollection<City> cityNames)
        {
            // Arrange

            var faker = new Faker();

            var cityEntities = cityNames
                .Select(name => new CityEntity
                {
                    Name = name,
                    Id = new CityIntId(faker.Random.Int(1, 1_000_000)),
                })
                .ToArray();

            // Act

            _dbContext.Cities.AddRange(cityEntities);
            _dbContext.SaveChanges();

            var cities = _dbContext.Cities.ToArray();

            // Assert

            Assert.That(cities.Length, Is.EqualTo(cityNames.Count));
        }

            [Test]
        [StrongAutoData]
        public void ShouldSucceed_SeedingData_ForRelatedData(IReadOnlyCollection<City> cityNames)
        {
            // Arrange

            var faker = new Faker();

            var cityEntities = cityNames
                .Select(name => new CityEntity
                {
                    Name = name,
                    Id = new CityIntId(faker.Random.Int(1, 1_000_000)),
                })
                .ToArray();

            var weatherForecastEntities = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecastEntity
                {
                    Id = new WeatherForecastGuidId(),
                    City = faker.PickRandom(cityEntities),
                    Email = new Email("bob@example.com"),
                    Date = DateTime.Now.AddDays(index),
                    Amount = (Amount)55,
                })
                .ToArray();

            // Act

            _dbContext.Cities.AddRange(cityEntities);
            _dbContext.WeatherForecasts.AddRange(weatherForecastEntities);
            _dbContext.SaveChanges();

            var cities = _dbContext.Cities.ToArray();
            var weatherForecasts = _dbContext.WeatherForecasts.ToArray();

            // Assert

            Assert.That(cities.Length, Is.EqualTo(cityNames.Count));
            Assert.That(weatherForecasts.Length, Is.EqualTo(weatherForecastEntities.Length));
        }

        [Test]
        public void ShouldSucceed_SeedingData_ForStructId()
        {
            // Arrange

            var internetFakerBuilder = new InternetFakerBuilder();
            var ipV4AddressFaker = internetFakerBuilder.BuildIpV4AddressFaker();
            var macAddressFaker = internetFakerBuilder.BuildMacAddressFaker();
            var emailFaker = internetFakerBuilder.BuildEmailFaker();
            var avatarUriFaker = internetFakerBuilder.BuildAvatarUriFaker();

            var entities = Enumerable.Range(1, 5)
                .Select(_ => new EmployeeEntity
                {
                    StructGuidId = EmployeeStructGuidId.New(),
                    Email = emailFaker.Generate(),
                    IpV4Address = ipV4AddressFaker.Generate(),
                    MacAddress = macAddressFaker.Generate(),
                    AvatarUri = avatarUriFaker.Generate(),
                })
                .ToArray();

            // Act

            _dbContext.Employees.AddRange(entities);
            _dbContext.SaveChanges();

            var employees = _dbContext.Employees.ToArray();

            // Assert

            Assert.That(employees.Length, Is.EqualTo(entities.Length));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterStronglyTypedConverters();

            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherForecast1> WeatherForecasts1 { get; set; }

        public DbSet<WeatherForecast2> WeatherForecasts2 { get; set; }

        public DbSet<WeatherForecast3> WeatherForecasts3 { get; set; }

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
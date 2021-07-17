using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Xtz.StronglyTyped.EntityFramework
{
    public static class ModelBuilderExtensions
    {
        private static readonly IReadOnlyCollection<Type> KNOWN_BASE_TYPES = new[]
        {
            typeof(bool),
            typeof(byte),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(short),
            typeof(string),
            typeof(uint),
            typeof(ulong),
            typeof(ushort),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
        };

        private static readonly Type DEFAULT_BASE_TYPE = typeof(string);

        /// <summary>
        /// Registers value converters for strong types.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <remarks>Use in <see cref="DbContext"/> method `protected override void OnModelCreating(ModelBuilder modelBuilder) { /* */ }`</remarks>
        public static void RegisterStronglyTypedConverters(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var propertyInfo in entityType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (IsSupportedType(propertyInfo))
                    {
                        var innerType = ExtractInnerType(propertyInfo.PropertyType!);
                        var valueConverter = BuildValueConverter(innerType, propertyInfo.PropertyType);

                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(propertyInfo.Name)
                            .HasConversion(valueConverter);

                        // ReSharper disable once RedundantJumpStatement
                        continue;
                    }

                    // Not supported
                }
            }
        }

        private static bool IsSupportedType(PropertyInfo propertyInfo)
        {
            return typeof(IStronglyTyped<string>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<char>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<sbyte>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<byte>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<ushort>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<short>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<uint>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<int>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<long>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<ulong>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<decimal>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<float>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<double>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<bool>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<TimeSpan>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<DateTime>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<Guid>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<Uri>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<MailAddress>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<IPAddress>).IsAssignableFrom(propertyInfo.PropertyType)
                || typeof(IStronglyTyped<PhysicalAddress>).IsAssignableFrom(propertyInfo.PropertyType);
        }

        private static Type ExtractInnerType(Type type)
        {
            try
            {
                var stronglyTypedInterface = type.GetInterface("IStronglyTyped`1");
                var result = stronglyTypedInterface?.GenericTypeArguments.FirstOrDefault();
                if (result == null) return DEFAULT_BASE_TYPE;

                return KNOWN_BASE_TYPES.Contains(result) ? result : DEFAULT_BASE_TYPE;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception '{e.GetType()}': {e.Message}");
                return DEFAULT_BASE_TYPE;
            }
        }

        private static ValueConverter BuildValueConverter(Type innerType, Type strongType)
        {
            var castingConverterType = typeof(CastingConverter<,>).MakeGenericType(strongType, innerType);
            ConverterMappingHints? converterMappingHints = null;
            var result = (ValueConverter)Activator.CreateInstance(castingConverterType, converterMappingHints);
            return result;
        }
    }
}

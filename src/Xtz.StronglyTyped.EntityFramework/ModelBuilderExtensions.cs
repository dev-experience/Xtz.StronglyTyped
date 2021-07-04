using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Xtz.StronglyTyped.EntityFramework
{
    public static class ModelBuilderExtensions
    {
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
                        var innerType = ExtractInnerType(propertyInfo.PropertyType.BaseType!);
                        var valueConverter = BuildValueConverter(innerType, propertyInfo.PropertyType);

                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(propertyInfo.Name)
                            .HasConversion(valueConverter);
                        continue;
                    }

                    // Not supported
                }
            }
        }

        private static bool IsSupportedType(PropertyInfo propertyInfo)
        {
            // TODO: Add `IPAddress`, `PhysicalAddress`, `Uri`, `MailAddress`
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
                || typeof(IStronglyTyped<Guid>).IsAssignableFrom(propertyInfo.PropertyType);
        }

        private static Type ExtractInnerType(Type innerType)
        {
            var currentType = innerType;
            while (currentType.BaseType != typeof(object) && currentType.BaseType == typeof(ValueType))
            {
                currentType = currentType.BaseType!;
            }

            var stronglyTypedInterface = currentType.GetInterface("IStronglyTyped`1");
            var result = stronglyTypedInterface?.GenericTypeArguments.FirstOrDefault();
            return result ?? typeof(string);

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

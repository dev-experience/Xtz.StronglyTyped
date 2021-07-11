using System;
using System.ComponentModel;
using System.Globalization;

namespace Xtz.StronglyTyped.TypeConverters
{
    public class StringTypeConverter<TStronglyTyped> : TypeConverter, ICustomTypeConverter
        where TStronglyTyped : IStronglyTyped<string>
    {
        public Type StrongType => typeof(TStronglyTyped);

        public Type InnerType => typeof(string);


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
        {
            if (value is null)
            {
                return null;
            }

            var strongType = typeof(TStronglyTyped);

            if (value is string stringValue)
            {
                return Activator.CreateInstance(strongType, stringValue);
            }

            throw new StringTypeConverterException(strongType, $"Can't convert from '{value.GetType().Name}' to '{strongType.Name}'");
        }
    }
}

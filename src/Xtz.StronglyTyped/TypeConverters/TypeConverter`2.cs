using System;
using System.ComponentModel;
using System.Globalization;

namespace Xtz.StronglyTyped.TypeConverters
{
    public class TypeConverter<TStronglyTyped, TInnerType> : TypeConverter, ICustomTypeConverter
        where TStronglyTyped : IStronglyTyped<TInnerType>
        where TInnerType : notnull
    {
        public Type StrongType => typeof(TStronglyTyped);

        public Type InnerType => typeof(TInnerType);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(TInnerType) || sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(TInnerType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
        {
            if (value is null)
            {
                return null;
            }

            var strongType = typeof(TStronglyTyped);

            TInnerType innerValue;

            if (value is string stringValue)
            {
                var innerType = typeof(TInnerType);
                var innerTypeConverter = TypeDescriptor.GetConverter(innerType);

                if (innerTypeConverter.CanConvertFrom(typeof(string)))
                {
                    innerValue = (TInnerType)innerTypeConverter.ConvertFrom(stringValue)!;
                }
                else
                {
                    return Activator.CreateInstance(strongType, stringValue);
                }
            }
            else if (value is TInnerType innerValue2)
            {
                innerValue = innerValue2;
            }
            else
            {
                throw new StronglyTypedException(strongType, $"Can't convert from '{value.GetType().Name}' to '{strongType.Name}'");
            }

            try
            {
                return Activator.CreateInstance(strongType, innerValue);
            }
            catch (Exception e)
            {
                throw new StronglyTypedException(strongType, $"Failed to create instance of '{strongType}'", e);
            }
        }
    }
}

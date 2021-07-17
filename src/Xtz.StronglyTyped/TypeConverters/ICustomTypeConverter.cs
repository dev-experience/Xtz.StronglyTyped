using System;

namespace Xtz.StronglyTyped.TypeConverters
{
    public interface ICustomTypeConverter
    {
        Type StrongType { get; }

        Type InnerType { get; }

        /// <summary>
        /// Converts the given value to the converter's native type.
        /// </summary>
        object ConvertFrom(object value);

        /// <summary>
        /// Converts the given value object to the specified destination type using the arguments.
        /// </summary>
        object ConvertTo(object value, Type destinationType);
    }
}
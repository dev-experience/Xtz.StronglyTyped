using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Xml;
using Newtonsoft.Json;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.NewtonsoftJson
{
    [SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "Vlad DX: Reviewed (more readable code)")]
    public class StronglyTypedNewtonsoftConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IStronglyTyped).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var typeConverter = TypeDescriptor.GetConverter(objectType);

            if (reader.Value is long longValue)
            {
                return ConvertFromLong(longValue, typeConverter as ICustomTypeConverter);
            }

            if (reader.Value is double doubleValue)
            {
                return ConvertFromDouble(doubleValue, typeConverter as ICustomTypeConverter);
            }

            if (reader.Value is BigInteger bigIntValue)
            {
                return ConvertFromBigInt(bigIntValue, typeConverter as ICustomTypeConverter);
            }

            if (typeConverter is ICustomTypeConverter customTypeConverter && customTypeConverter.InnerType == typeof(TimeSpan))
            {
                return ConvertToTimeSpan(reader.Value, typeConverter);
            }

            return (IStronglyTyped)typeConverter.ConvertFrom(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            var typeConverter = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter is ICustomTypeConverter customTypeConverter)
            {
                if (customTypeConverter.InnerType == typeof(bool))
                {
                    var boolValue = (value as IStronglyTyped<bool>)?.Value ?? default(bool);
                    writer.WriteValue(boolValue);
                    return;
                }

                if (TryWriteNumber(value, customTypeConverter.InnerType, writer))
                {
                    return;
                }
            }

            var stringValue = typeConverter.ConvertTo(value, typeof(string)) as string;
            writer.WriteValue(stringValue);
        }

        private IStronglyTyped ConvertFromLong(long longValue, ICustomTypeConverter typeConverter)
        {
            if (typeConverter.InnerType == typeof(byte))
            {
                var value = unchecked((byte)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(decimal))
            {
                var value = (decimal)longValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(double))
            {
                var value = (double)longValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(float))
            {
                var value = (float)longValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(int))
            {
                var value = unchecked((int)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(long))
            {
                var result = (typeConverter as TypeConverter)!.ConvertFrom(longValue);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(sbyte))
            {
                var value = unchecked((sbyte)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(short))
            {
                var value = unchecked((short)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(uint))
            {
                var value = unchecked((uint)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(ulong))
            {
                var value = unchecked((ulong)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(ushort))
            {
                var value = unchecked((ushort)longValue);
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            throw new NewtonsoftJsonConverterException(typeConverter.StrongType, $"Can't convert from '{typeof(long)}' to '{typeConverter.StrongType.FullName}'");
        }

        private IStronglyTyped ConvertFromDouble(double doubleValue, ICustomTypeConverter typeConverter)
        {
            if (typeConverter.InnerType == typeof(double))
            {
                var result = (typeConverter as TypeConverter)!.ConvertFrom(doubleValue);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(decimal))
            {
                var value = (decimal)doubleValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(float))
            {
                var value = (float)doubleValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            throw new NewtonsoftJsonConverterException(typeConverter.StrongType, $"Can't convert from '{typeof(double)}' to '{typeConverter.StrongType.FullName}'");
        }

        private IStronglyTyped ConvertFromBigInt(BigInteger bigIntValue, ICustomTypeConverter typeConverter)
        {
            if (typeConverter.InnerType == typeof(decimal))
            {
                var value = (decimal)bigIntValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            if (typeConverter.InnerType == typeof(ulong))
            {
                var value = (ulong)bigIntValue;
                var result = (typeConverter as TypeConverter)!.ConvertFrom(value);
                return (IStronglyTyped)result!;
            }

            throw new NewtonsoftJsonConverterException(typeConverter.StrongType, $"Can't convert from '{typeof(BigInteger)}' to '{typeConverter.StrongType.FullName}'");
        }

        private IStronglyTyped ConvertToTimeSpan(object value, TypeConverter typeConverter)
        {
            var timeSpanValue = XmlConvert.ToTimeSpan(value.ToString());
            var result = typeConverter.ConvertFrom(timeSpanValue);
            return (IStronglyTyped)result;
        }

        private bool TryWriteNumber(object value, Type innerType, JsonWriter writer)
        {
            if (innerType == typeof(decimal))
            {
                var decimalValue = (value as IStronglyTyped<decimal>)?.Value ?? default(decimal);
                writer.WriteValue(decimalValue);
                return true;
            }

            if (innerType == typeof(double))
            {
                var doubleValue = (value as IStronglyTyped<double>)?.Value ?? default(double);
                writer.WriteValue(doubleValue);
                return true;
            }

            if (innerType == typeof(float))
            {
                var floatValue = (value as IStronglyTyped<float>)?.Value ?? default(float);
                writer.WriteValue(floatValue);
                return true;
            }

            if (innerType == typeof(int))
            {
                var intValue = (value as IStronglyTyped<int>)?.Value ?? default(int);
                writer.WriteValue(intValue);
                return true;
            }

            if (innerType == typeof(long))
            {
                var longValue = (value as IStronglyTyped<long>)?.Value ?? default(long);
                writer.WriteValue(longValue);
                return true;
            }

            if (innerType == typeof(uint))
            {
                var uintValue = (value as IStronglyTyped<uint>)?.Value ?? default(uint);
                writer.WriteValue(uintValue);
                return true;
            }

            if (innerType == typeof(ulong))
            {
                var ulongValue = (value as IStronglyTyped<ulong>)?.Value ?? default(ulong);
                writer.WriteValue(ulongValue);
                return true;
            }

            if (innerType == typeof(byte))
            {
                var byteValue = (value as IStronglyTyped<byte>)?.Value ?? default(byte);
                writer.WriteValue(byteValue);
                return true;
            }

            if (innerType == typeof(sbyte))
            {
                var sbyteValue = (value as IStronglyTyped<sbyte>)?.Value ?? default(sbyte);
                writer.WriteValue(sbyteValue);
                return true;
            }

            if (innerType == typeof(short))
            {
                var shortValue = (value as IStronglyTyped<short>)?.Value ?? default(short);
                writer.WriteValue(shortValue);
                return true;
            }

            if (innerType == typeof(ushort))
            {
                var ushortValue = (value as IStronglyTyped<ushort>)?.Value ?? default(ushort);
                writer.WriteValue(ushortValue);
                return true;
            }

            return false;
        }
    }
}
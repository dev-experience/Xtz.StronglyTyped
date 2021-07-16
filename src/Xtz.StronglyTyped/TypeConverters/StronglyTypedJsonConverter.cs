using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Xtz.StronglyTyped.TypeConverters
{
    public class StronglyTypedJsonConverter<TStronglyTyped> : JsonConverter<TStronglyTyped>
        where TStronglyTyped : IStronglyTyped
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IStronglyTyped).IsAssignableFrom(typeToConvert);
        }

        public override TStronglyTyped? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var typeConverter = TypeDescriptor.GetConverter(typeToConvert);

            if (typeConverter is ICustomTypeConverter customTypeConverter)
            {
                if (customTypeConverter.InnerType == typeof(TimeSpan))
                {
                    return (TStronglyTyped)typeConverter.ConvertFrom(XmlConvert.ToTimeSpan(reader.GetString()));
                }

                if (customTypeConverter.InnerType == typeof(DateTime))
                {
                    return ReadDateTime(reader, customTypeConverter.StrongType, typeConverter);
                }

                if (reader.TokenType is JsonTokenType.True or JsonTokenType.False)
                {
                    return (TStronglyTyped)typeConverter.ConvertFrom(reader.GetBoolean());
                }

                if (reader.TokenType == JsonTokenType.Number)
                {
                    return ReadNumber(reader, customTypeConverter.StrongType, customTypeConverter.InnerType, typeConverter);
                }
            }

            var stringValue = reader.GetString();
            return (TStronglyTyped)typeConverter.ConvertFrom(stringValue);
        }

        public override void Write(Utf8JsonWriter writer, TStronglyTyped? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            var typeConverter = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter is ICustomTypeConverter customTypeConverter)
            {
                if (customTypeConverter.InnerType == typeof(bool))
                {
                    var boolValue = (value as IStronglyTyped<bool>)?.Value ?? default(bool);
                    writer.WriteBooleanValue(boolValue);
                    return;
                }

                if (TryWriteNumber(value, customTypeConverter.InnerType, writer))
                {
                    return;
                }
            }

            var stringValue = typeConverter.ConvertTo(value, typeof(string)) as string;
            writer.WriteStringValue(stringValue);
        }

        private TStronglyTyped? ReadNumber(Utf8JsonReader reader, Type strongType, Type innerType, TypeConverter typeConverter)
        {

            if (innerType == typeof(int))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetInt32());
            }

            if (innerType == typeof(float))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetSingle());
            }

            if (innerType == typeof(decimal))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetDecimal());
            }

            if (innerType == typeof(long))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetInt64());
            }

            if (innerType == typeof(double))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetDouble());
            }

            if (innerType == typeof(byte))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetByte());
            }

            if (innerType == typeof(sbyte))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetSByte());
            }

            if (innerType == typeof(short))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetInt16());
            }

            if (innerType == typeof(uint))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetUInt32());
            }

            if (innerType == typeof(ulong))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetUInt64());
            }

            if (innerType == typeof(ushort))
            {
                return (TStronglyTyped) typeConverter.ConvertFrom(reader.GetUInt16());
            }

            throw new JsonConverterException(strongType, $"Can't convert value to '{strongType.FullName}'");
        }

        private TStronglyTyped? ReadDateTime(Utf8JsonReader reader, Type strongType, TypeConverter typeConverter)
        {
            if (reader.TryGetDateTime(out var dateTimeValue))
            {
                return (TStronglyTyped)typeConverter.ConvertFrom(dateTimeValue);
            }

            var stringValue = reader.GetString();
            if (DateTime.TryParse(stringValue, out var dateTimeValue2))
            {
                return (TStronglyTyped)typeConverter.ConvertFrom(dateTimeValue2);
            }
        
            throw new JsonConverterException(strongType, $"Can't convert from '{stringValue}' to '{strongType.FullName}'");
        }

        private IStronglyTyped ConvertToTimeSpan(string value, TypeConverter typeConverter)
        {
            var timeSpanValue = XmlConvert.ToTimeSpan(value.ToString());
            var result = typeConverter.ConvertFrom(timeSpanValue);
            return (IStronglyTyped)result;
        }

        private bool TryWriteNumber(IStronglyTyped value, Type innerType, Utf8JsonWriter writer)
        {
            if (innerType == typeof(decimal))
            {
                var decimalValue = (value as IStronglyTyped<decimal>)?.Value ?? default(decimal);
                writer.WriteNumberValue(decimalValue);
                return true;
            }

            if (innerType == typeof(double))
            {
                var doubleValue = (value as IStronglyTyped<double>)?.Value ?? default(double);
                writer.WriteNumberValue(doubleValue);
                return true;
            }

            if (innerType == typeof(float))
            {
                var floatValue = (value as IStronglyTyped<float>)?.Value ?? default(float);
                writer.WriteNumberValue(floatValue);
                return true;
            }

            if (innerType == typeof(int))
            {
                var intValue = (value as IStronglyTyped<int>)?.Value ?? default(int);
                writer.WriteNumberValue(intValue);
                return true;
            }

            if (innerType == typeof(long))
            {
                var longValue = (value as IStronglyTyped<long>)?.Value ?? default(long);
                writer.WriteNumberValue(longValue);
                return true;
            }

            if (innerType == typeof(uint))
            {
                var uintValue = (value as IStronglyTyped<uint>)?.Value ?? default(uint);
                writer.WriteNumberValue(uintValue);
                return true;
            }

            if (innerType == typeof(ulong))
            {
                var ulongValue = (value as IStronglyTyped<ulong>)?.Value ?? default(ulong);
                writer.WriteNumberValue(ulongValue);
                return true;
            }

            if (innerType == typeof(byte))
            {
                var byteValue = (value as IStronglyTyped<byte>)?.Value ?? default(byte);
                writer.WriteNumberValue(byteValue);
                return true;
            }

            if (innerType == typeof(sbyte))
            {
                var sbyteValue = (value as IStronglyTyped<sbyte>)?.Value ?? default(sbyte);
                writer.WriteNumberValue(sbyteValue);
                return true;
            }

            if (innerType == typeof(short))
            {
                var shortValue = (value as IStronglyTyped<short>)?.Value ?? default(short);
                writer.WriteNumberValue(shortValue);
                return true;
            }

            if (innerType == typeof(ushort))
            {
                var ushortValue = (value as IStronglyTyped<ushort>)?.Value ?? default(ushort);
                writer.WriteNumberValue(ushortValue);
                return true;
            }

            return false;
        }
    }
}
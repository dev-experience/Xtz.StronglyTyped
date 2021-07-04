using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var stringValue = reader.GetString();
            var typeConverter = TypeDescriptor.GetConverter(typeToConvert);

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
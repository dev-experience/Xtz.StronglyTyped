using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Xtz.StronglyTyped.Api_3_1.IntegrationTests.WebApi
{
    public class StronglyTypedNewtonsoftConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IStronglyTyped).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var stringValue = reader.ReadAsString();
            var typeConverter = TypeDescriptor.GetConverter(objectType);

            return (IStronglyTyped)typeConverter.ConvertFrom(stringValue);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteValue("null");
                return;
            }

            var typeConverter = TypeDescriptor.GetConverter(value.GetType());

            var stringValue = typeConverter.ConvertTo(value, typeof(string)) as string;
            writer.WriteValue(stringValue);
        }
    }
}
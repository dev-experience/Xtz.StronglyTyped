using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.Swashbuckle
{
    /// <summary>
    /// Swashbuckle Schema Filter to override <see cref="OpenApiSchema"/> for strong types.
    /// </summary>
    public class StronglyTypedSchemaFilter : ISchemaFilter
    {
        // TODO: Replace strings by strong types
        private readonly Dictionary<Type, (string, string?)> TYPE_MAPPING = new()
        {
            { typeof(string), ("string", "string") },
            { typeof(bool), ("boolean", null) },
            { typeof(int), ("integer", "int32") },
            { typeof(long), ("integer", "int64") },
            { typeof(double), ("number", "double") },
            { typeof(float), ("number", "float") },
            { typeof(uint), ("integer", "int32") },
            { typeof(ulong), ("integer", "int64") },
            { typeof(byte), ("integer", "int32") },
            { typeof(sbyte), ("integer", "int32") },
            { typeof(short), ("integer", "int32") },
            { typeof(ushort), ("integer", "int32") },
            { typeof(DateTime), ("string", "date-time") },
            { typeof(Guid), ("string", "uuid") },
            { typeof(MailAddress), ("string", "email") },
            { typeof(Uri), ("string", "uri") },
        };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!typeof(IStronglyTyped).IsAssignableFrom(context.Type))
            {
                return;
            }

            var typeConverter = TypeDescriptor.GetConverter(context.Type);
            if (typeConverter is ICustomTypeConverter customTypeConverter)
            {
                var (type, format) = MapType(customTypeConverter.InnerType);
                if (format == "string")
                {
                    format = context.Type.Name;
                }

                schema.Type = type;
                schema.Format = format;
                schema.Properties.Clear();
                schema.AdditionalPropertiesAllowed = true;
            }
        }

        private (string, string?) MapType(Type innerType)
        {
            if (TYPE_MAPPING.TryGetValue(innerType, out var result))
            {
                return result;
            }

            return ("string", null);
        }
    }
}
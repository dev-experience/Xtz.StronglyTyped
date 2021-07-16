using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    public class JsonConverterException : StronglyTypedException
    {
        public JsonConverterException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }
    }
}
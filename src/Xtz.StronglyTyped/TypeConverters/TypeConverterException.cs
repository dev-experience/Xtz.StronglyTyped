using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    public class TypeConverterException : StronglyTypedException
    {
        public TypeConverterException(Type type, string errorMessage) : base(type, errorMessage)
        {
        }

        public TypeConverterException(Type type, string errorMessage, Exception innerException) : base(type, errorMessage, innerException)
        {
        }
    }
}
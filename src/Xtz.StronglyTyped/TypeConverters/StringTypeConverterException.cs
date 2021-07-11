using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    public class StringTypeConverterException : StronglyTypedException
    {
        public StringTypeConverterException(Type type, string errorMessage) : base(type, errorMessage)
        {
        }
    }
}
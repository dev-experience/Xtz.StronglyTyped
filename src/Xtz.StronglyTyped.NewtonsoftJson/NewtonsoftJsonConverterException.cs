using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.NewtonsoftJson
{
    [ExcludeFromCodeCoverage]
    public class NewtonsoftJsonConverterException : StronglyTypedException
    {
        public NewtonsoftJsonConverterException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }
    }
}
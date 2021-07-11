using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    [ExcludeFromCodeCoverage]
    public class FakerBuilderException : ApplicationException
    {
        public FakerBuilderException(string message) : base(message)
        {
        }

        public FakerBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
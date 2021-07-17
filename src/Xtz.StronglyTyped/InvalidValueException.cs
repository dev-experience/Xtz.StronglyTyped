using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped
{
    [ExcludeFromCodeCoverage]
    public class InvalidValueException : StronglyTypedException
    {
        public InvalidValueException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }
    }
}
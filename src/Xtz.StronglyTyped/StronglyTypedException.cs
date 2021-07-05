using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped
{
    [ExcludeFromCodeCoverage]
    public class StronglyTypedException : Exception
    {
        public Type Type { get; }

        public StronglyTypedException(Type type, string errorMessage)
            : base(errorMessage)
        {
            Type = type;
        }

        public StronglyTypedException(Type type, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            Type = type;
        }
    }
}

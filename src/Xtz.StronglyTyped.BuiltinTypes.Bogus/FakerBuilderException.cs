using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class FakerBuilderException : ApplicationException
    {
        public FakerBuilderException(string message)
            : base(message)
        {
        }

        public FakerBuilderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected FakerBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
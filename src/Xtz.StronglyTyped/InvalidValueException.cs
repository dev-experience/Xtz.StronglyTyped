using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidValueException : StronglyTypedException
    {
        public InvalidValueException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected InvalidValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
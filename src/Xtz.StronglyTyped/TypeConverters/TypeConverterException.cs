using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class TypeConverterException : StronglyTypedException
    {
        public TypeConverterException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }

        public TypeConverterException(Type type, string errorMessage, Exception innerException)
            : base(type, errorMessage, innerException)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected TypeConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
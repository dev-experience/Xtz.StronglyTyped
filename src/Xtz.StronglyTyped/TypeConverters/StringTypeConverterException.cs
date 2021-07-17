using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class StringTypeConverterException : StronglyTypedException
    {
        public StringTypeConverterException(Type type, string errorMessage) : base(type, errorMessage)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected StringTypeConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
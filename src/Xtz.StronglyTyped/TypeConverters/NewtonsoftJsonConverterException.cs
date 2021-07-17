using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.TypeConverters
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class JsonConverterException : StronglyTypedException
    {
        public JsonConverterException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected JsonConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
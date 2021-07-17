using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.NewtonsoftJson
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class NewtonsoftJsonConverterException : StronglyTypedException
    {
        public NewtonsoftJsonConverterException(Type type, string errorMessage)
            : base(type, errorMessage)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected NewtonsoftJsonConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
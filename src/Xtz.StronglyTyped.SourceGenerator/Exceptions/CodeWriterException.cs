using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CodeWriterException : ApplicationException
    {
        public CodeWriterException(string message)
            : base(message)
        {
        }

        public CodeWriterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected CodeWriterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
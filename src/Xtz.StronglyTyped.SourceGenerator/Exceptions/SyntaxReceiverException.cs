using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class SyntaxReceiverException : ApplicationException
    {
        public SyntaxReceiverException(string message)
            : base(message)
        {
        }

        public SyntaxReceiverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected SyntaxReceiverException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
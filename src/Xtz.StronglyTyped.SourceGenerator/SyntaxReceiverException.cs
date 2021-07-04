using System;

namespace Xtz.StronglyTyped.SourceGenerator
{
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
    }
}
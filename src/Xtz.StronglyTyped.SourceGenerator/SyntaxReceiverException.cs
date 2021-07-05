using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
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
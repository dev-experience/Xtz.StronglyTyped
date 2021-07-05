using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
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
    }
}
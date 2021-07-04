using System;

namespace Xtz.StronglyTyped.SourceGenerator
{
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
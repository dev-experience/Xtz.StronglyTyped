using System;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class GeneratorTestsException : ApplicationException
    {
        public GeneratorTestsException(string message)
            : base(message)
        {
        }

        public GeneratorTestsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
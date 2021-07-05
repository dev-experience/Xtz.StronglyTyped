using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    [ExcludeFromCodeCoverage]
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
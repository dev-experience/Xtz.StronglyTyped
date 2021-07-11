using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class TestsExecutionException : ApplicationException
    {
        public TestsExecutionException(string message)
            : base(message)
        {
        }

        public TestsExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
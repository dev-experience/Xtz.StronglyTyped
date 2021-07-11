using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class InnerTypesGeneratorTests : GeneratorTestsBase
    {
        [Test]
        [TestCase(typeof(bool))]
        [TestCase(typeof(byte))]
        [TestCase(typeof(char))]
        [TestCase(typeof(decimal))]
        [TestCase(typeof(double))]
        [TestCase(typeof(float))]
        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(sbyte))]
        [TestCase(typeof(short))]
        [TestCase(typeof(string))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(ushort))]
        public void ShouldGenerate_ForPrimitiveTypes(Type innerType)
        {
            //// Arrange

            var sourceCode = $@"
namespace IntegrationTests.Generated
{{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof({innerType.FullName}))]
    public partial class WithInnerType
    {{
    }}
}}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            AssertGenerationSuccess(4, diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(TimeSpan))]
        [TestCase(typeof(Guid))]
        [TestCase(typeof(Uri))]
        [TestCase(typeof(MailAddress))]
        [TestCase(typeof(IPAddress))]
        [TestCase(typeof(PhysicalAddress))]
        public void ShouldGenerate_ForKnownTypes(Type innerType)
        {
            //// Arrange

            var sourceCode = $@"
namespace IntegrationTests.Generated
{{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof({innerType.FullName}))]
    public partial class WithInnerType
    {{
    }}
}}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            AssertGenerationSuccess(4, diagnostics, outputCompilation, driver.GetRunResult());
        }
    }
}
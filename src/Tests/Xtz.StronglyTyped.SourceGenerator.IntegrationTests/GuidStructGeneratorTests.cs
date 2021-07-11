using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class GuidStructGeneratorTests : GeneratorTestsBase
    {
        [Test]
        public void ShouldGenerate_StringConstructor()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(System.Guid))]
    public partial struct CityGuidStruct2
    {
    }

    public class CompilationTest
    {
        public CityGuidStruct2 Test() => new CityGuidStruct2(""115a0250-d05d-4225-9331-a1d238a0f608"");
    }
}
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
        public void ShouldNotFail_WhenToStringExists()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(System.Guid))]
    public partial struct CityGuidStruct2
    {
        public override string ToString() => $""{Value:B}"";
    }

    public class CompilationTest
    {
        public CityGuidStruct2 Test() => new CityGuidStruct2(""115a0250-d05d-4225-9331-a1d238a0f608"");
    }
}
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
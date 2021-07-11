using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class BasicGeneratorTests : GeneratorTestsBase
    {
        [Test]
        public void ShouldGeneratePartialStruct_WhenAttributeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType]
    public partial struct City2
    {
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
        public void ShouldGeneratePartialClass_WhenAttributeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType]
    public partial class City2
    {
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
        public void ShouldFail_WhenNoAttributeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var city = (City2)""Amsterdam"";

            return 0;
        }
    }

    public partial class City2
    {
    }
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(4, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsFalse(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldFail_WhenMissingPartialKeyword()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    public class Program
    {
        public static int Main(string[] args)
        {
            var city = (City2)""Amsterdam"";

            return 0;
        }
    }

    [StrongType]
    public class City2
    {
    }
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(5, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsFalse(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldNotGenerate_WhenAttributeProvided_ForExoticSystemInnerType()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(System.Attribute))]
    public partial class City2
    {
    }
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(5, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsFalse(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldNotGenerate_WhenNoNamespace()
        {
            //// Arrange

            var sourceCode = @"
using Xtz.StronglyTyped.SourceGenerator;

[StrongType]
public partial class City2
{
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(4, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldNotGenerate_WhenAttributeProvided_ForRecordType()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType]
    public partial record City2
    {
    }
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(4, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldNotGenerate_WhenAttributeProvided_ForExoticInnerType()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    public class ExoticType
    {
    }

    [StrongType(typeof(ExoticType))]
    public partial class City2
    {
    }
}
            ";

            var inputCompilation = CreateCompilation(sourceCode, OutputKind.DynamicallyLinkedLibrary);

            //// Act

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new StronglyTypedGenerator());

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            //// Assert

            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(4, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty);
        }
    }
}
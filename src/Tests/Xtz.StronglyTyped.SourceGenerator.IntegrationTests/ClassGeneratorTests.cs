using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class ClassGeneratorTests : GeneratorTestsBase
    {
        //// TODO: Test with all supported inner types (primitive + Guid)
        [Test]
        public void ShouldGeneratePartialClass_WhenAttributeWithInnerTypeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(decimal))]
    public partial class DegreesCelsius2 : System.IEquatable<DegreesCelsius2>
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
        public void ShouldGeneratePartialClass_WhenDerivedFromOtherClass()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;
    using Xtz.StronglyTyped.SourceGenerator;

    public abstract class CustomGuidId : StronglyTyped<System.Guid>
    {
        protected CustomGuidId(System.Guid value)
            : base(value)
        {
        }
    }

    [StrongType(typeof(System.Guid))]
    public partial class EmployeeGuidId : CustomGuidId
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
        public void ShouldGenerateEmptyClassConstructor_WhenInnerTypeIsSpecial()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(System.Guid))]
    public partial class ExternalId
    {
    }

    public class CompilationTest
    {
        public void Test()
        {
            var x = new ExternalId();
        }
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
        public void ShouldNotGenerateStringConstructor_WhenAlreadyProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;
    using Xtz.StronglyTyped.SourceGenerator;
    using System.Net.Mail;

    [StrongType(typeof(MailAddress))]
    public partial class Email
    {
        public Email(System.String value)
            : base(new MailAddress(value))
        {
            if (value.Contains(' ')) throw new StronglyTypedException(typeof(Email), ""Space characters are not allowed in email"");
        }
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
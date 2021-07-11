using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class CompileAndRunGeneratorTests : GeneratorTestsBase
    {
        [Test]
        public void ShouldSucceed_For3Types()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;
    using IntegrationTests.WeatherForecast;

    public class Program
    {
        public static int Main(string[] args)
        {
            var city = (City2)""Amsterdam"";
            var city2 = (CityStruct2)""Amsterdam"";
            var temperature = (DegreesCelsius3)22.7;

            return 0;
        }
    }

    [StrongType]
    public partial class City2
    {
    }

    [StrongType]
    public partial struct CityStruct2
    {
    }
}

namespace IntegrationTests.WeatherForecast
{
    using Xtz.StronglyTyped.SourceGenerator;

    [StrongType(typeof(int))]
    public partial class DegreesCelsius3
    {
    }
}

";
            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

            //// Act

            // directly create an instance of the generator
            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
            var generator = new StronglyTypedGenerator();

            // Create the driver that will control the generation, passing in our generator
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


            var compiledBytes = CompileBytes(outputCompilation);

            var result = LoadAndExecute(compiledBytes);

            // Assert

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ShouldThrow_WhenMissingPartialKeyword()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;
    using IntegrationTests.WeatherForecast;

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
            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

            //// Act

            // directly create an instance of the generator
            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
            var generator = new StronglyTypedGenerator();

            // Create the driver that will control the generation, passing in our generator
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            Assert.Throws<GeneratorTestsException>(Action);
        }
    }
}
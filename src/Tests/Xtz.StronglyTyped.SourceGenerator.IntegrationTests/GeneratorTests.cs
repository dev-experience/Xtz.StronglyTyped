using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyModel;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    public class GeneratorTests
    {
        [Test]
        public void ShouldGeneratePartialClass_WhenAttributeProvided()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        public void ShouldGeneratePartialStruct_WhenAttributeProvided()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        public void ShouldGenerateThrowIfInvalid_WhenIsValidMethodProvided()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

    [StrongType]
    public partial struct City2
    {
        public bool IsValid(City2 value)
        {
            return false;
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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        //// TODO: Test with all supported inner types (primitive + Guid)
        [Test]
        public void ShouldGeneratePartialClass_WhenAttributeWithInnerTypeProvided()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        public void ShouldGeneratePartialClass_WhenDerivedFromOtherClass()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        public void ShouldGenerateEmptyClassConstructor_WhenInnerTypeIsSpecial()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;

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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
        }

        [Test]
        public void ShouldNotGenerateStringConstructor_WhenAlreadyProvided()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;
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

            AssertGenerationSuccess(diagnostics, outputCompilation, driver.GetRunResult());
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

            var diagnosticsMessages = outputCompilation.GetDiagnostics().Select(x => x.ToString());
            Debug.WriteLine(string.Join("\n", diagnosticsMessages));
            Assert.IsFalse(outputCompilation.GetDiagnostics().IsEmpty);
        }

        [Test]
        public void ShouldCompileAndRun()
        {
            //// Arrange

            // Create the 'input' compilation that the generator will act on
            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped;
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
    using Xtz.StronglyTyped;

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

        private static Compilation CreateCompilation(string source, OutputKind outputKind)
            => CSharpCompilation.Create(
                "compilation",
                new[] { CSharpSyntaxTree.ParseText(source) },
                BuildReferences(),
                new CSharpCompilationOptions(outputKind));

        private static PortableExecutableReference[] BuildReferences()
        {
            //var netstandard = Assembly.Load("netstandard");
            //var mscorlib = Assembly.Load("mscorlib");

            var directReferences = new[]
            {
                //netstandard,
                //mscorlib,
                //typeof(Attribute).Assembly,
                typeof(IStronglyTyped).Assembly,
                //typeof(Binder).GetTypeInfo().Assembly,
                //typeof(Console).Assembly,
            };

            var transientReferences = typeof(IStronglyTyped).Assembly.GetReferencedAssemblies()
                .Select(x => Assembly.Load(x));

            var refs =
                DependencyContext.Default.CompileLibraries
                    .SelectMany(cl => cl.ResolveReferencePaths())
                    .Select(asm => MetadataReference.CreateFromFile(asm))
                    .ToArray();

            var result = directReferences
                .Union(transientReferences)
                .Select(x => MetadataReference.CreateFromFile(x.Location))
                .Union(refs)
                .ToArray();

            return result;
        }

        private static byte[] CompileBytes(Compilation compilation)
        {
            using var peStream = new MemoryStream();
            var result = compilation.Emit(peStream);

            if (!result.Success)
            {
                var failures = result.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error)
                    .ToImmutableArray();

                var errorBuilder = new StringBuilder();

                foreach (var diagnostic in failures)
                {
                    errorBuilder.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
                }

                var errorMessage = errorBuilder.ToString();
                throw new GeneratorTestsException(errorMessage);
            }

            peStream.Seek(0, SeekOrigin.Begin);

            var compiledBytes = peStream.ToArray();
            return compiledBytes;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static object LoadAndExecute(byte[] compiledAssembly, params string[] args)
        {
            using var assemblyStream = new MemoryStream(compiledAssembly);
            var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

            var assembly = assemblyLoadContext.LoadFromStream(assemblyStream);

            var entryPoint = assembly.EntryPoint;
            if (entryPoint == null)
            {
                throw new GeneratorTestsException("Entry point is not found");
            }

            var result  = entryPoint.GetParameters().Length > 0
                ? entryPoint.Invoke(null, new object[] { args })
                : entryPoint.Invoke(null, null);

            assemblyLoadContext.Unload();

            //return new WeakReference(assemblyLoadContext);

            return result;
        }

        private static void AssertGenerationSuccess(ImmutableArray<Diagnostic> diagnostics, Compilation outputCompilation,
            GeneratorDriverRunResult runResult)
        {
            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(5, outputCompilation.SyntaxTrees.Count());

            var diagnosticsMessages = outputCompilation.GetDiagnostics().Select(x => x.ToString());
            Debug.WriteLine(string.Join("\n", diagnosticsMessages));
            Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty);

            // A generated syntax tree and logs
            Assert.AreEqual(4, runResult.GeneratedTrees.Length);
            Assert.IsTrue(runResult.Diagnostics.IsEmpty);

            // Asserting the individual results on a by-generator basis
            var generatorResult = runResult.Results[0];
            Assert.AreEqual(typeof(StronglyTypedGenerator), generatorResult.Generator.GetType());
            Assert.IsTrue(generatorResult.Diagnostics.IsEmpty);
            Assert.AreEqual(4, generatorResult.GeneratedSources.Length);
            Assert.IsNull(generatorResult.Exception);
        }
    }
}
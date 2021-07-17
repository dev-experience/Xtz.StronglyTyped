using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    // TODO: Write tests for incomplete code (e.g. `[StrongType(]`) to test that generator ignores such cases

    public class CustomizedGeneratorTests : GeneratorTestsBase
    {
        [Test]
        public void ShouldGenerate_NoNull_ByDefault()
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
            var city = new City2(null);
            return 0;
        }
    }

    [StrongType]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_ByDefault()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenStringTypeProvided()
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
            var city = new CityUri2(null);
            return 0;
        }
    }

    [StrongType(typeof(string))]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_WhenStringTypeProvided()
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
            var city = new CityUri2("""");
            return 0;
        }
    }

    [StrongType(typeof(string))]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

////        [Test]
////        public void ShouldGenerate_NoEmpty_WhenGuidTypeProvided()
////        {
////            //// Arrange

////            var sourceCode = @"
////namespace IntegrationTests.Generated
////{
////    using Xtz.StronglyTyped.SourceGenerator;

////    public class Program
////    {
////        public static int Main(string[] args)
////        {
////            var city = new CityGuid2(System.Guid.Empty);
////            return 0;
////        }
////    }

////    [StrongType(typeof(System.Guid))]
////    public partial class CityGuid2
////    {
////    }
////}
////            ";

////            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

////            //// Act

////            // directly create an instance of the generator
////            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
////            var generator = new StronglyTypedGenerator();

////            // Create the driver that will control the generation, passing in our generator
////            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

////            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
////            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


////            var compiledBytes = CompileBytes(outputCompilation);

////            [ExcludeFromCodeCoverage]
////            void Action()
////            {
////                var compiledBytes = CompileBytes(outputCompilation);
////                LoadAndExecute(compiledBytes);
////            }

////            //// Assert

////            var exception = Assert.Throws<TestsExecutionException>(Action);
////            Assert.IsInstanceOf<InvalidValueException>(exception?.InnerException);
////            Assert.IsTrue(exception?.InnerException?.Message.Contains("''"));
////        }

        [Test]
        public void ShouldGenerate_NoNull_WhenUriTypeProvided()
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
            var city = new CityUri2((System.Uri)null);
            return 0;
        }
    }

    [StrongType(typeof(System.Uri))]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenMailAddressTypeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;
    using System.Net.Mail;

    public class Program
    {
        public static int Main(string[] args)
        {
            var city = new CityEmail2((MailAddress)null);
            return 0;
        }
    }

    [StrongType(typeof(MailAddress))]
    public partial class CityEmail2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenIPAddressTypeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;
    using System.Net;

    public class Program
    {
        public static int Main(string[] args)
        {
            var city = new CityIpAddress2((IPAddress)null);
            return 0;
        }
    }

    [StrongType(typeof(IPAddress))]
    public partial class CityIpAddress2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenPhysicalAddressTypeProvided()
        {
            //// Arrange

            var sourceCode = @"
namespace IntegrationTests.Generated
{
    using Xtz.StronglyTyped.SourceGenerator;
    using System.Net.NetworkInformation;

    public class Program
    {
        public static int Main(string[] args)
        {
            var city = new CityMacAddress2((PhysicalAddress)null);
            return 0;
        }
    }

    [StrongType(typeof(PhysicalAddress))]
    public partial class CityMacAddress2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenAllowUnknownProvided()
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
            var city = new City2(null);
            return 0;
        }
    }

    [StrongType(Allow.Unknown)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_WhenAllowUnknownProvided()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType(Allow.Unknown)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenStringTypeAllowUnknownProvided()
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
            var city = new CityUri2(null);
            return 0;
        }
    }

    [StrongType(typeof(string), Allow.Unknown)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_WhenStringTypeAllowUnknownProvided()
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
            var city = new CityUri2("""");
            return 0;
        }
    }

    [StrongType(typeof(string), Allow.Unknown)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenInvalidAllowProvided()
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
            var city = new CityUri2(null);
            return 0;
        }
    }

    [StrongType((Allow)999)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_WhenInvalidAllowProvided()
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
            var city = new CityUri2("""");
            return 0;
        }
    }

    [StrongType((Allow)999)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoNull_WhenStringTypeInvalidAllowProvided()
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
            var city = new CityUri2(null);
            return 0;
        }
    }

    [StrongType(typeof(string), (Allow)999)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("<null>"), Is.True);
        }

        [Test]
        public void ShouldGenerate_NoEmpty_WhenStringTypeInvalidAllowProvided()
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
            var city = new CityUri2("""");
            return 0;
        }
    }

    [StrongType(typeof(string), (Allow)999)]
    public partial class CityUri2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            [ExcludeFromCodeCoverage]
            void Action()
            {
                var compiledBytes = CompileBytes(outputCompilation);
                LoadAndExecute(compiledBytes);
            }

            //// Assert

            var exception = Assert.Throws<TestsExecutionException>(Action);
            Assert.That(exception?.InnerException, Is.InstanceOf<InvalidValueException>());
            Assert.That(exception?.InnerException?.Message.Contains("''"), Is.True);
        }

//        [Test]
//        public void ShouldGenerate_AllowNull_WhenAllowNullProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2(null);
//            return 0;
//        }
//    }

//    [StrongType(Allow.Null)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }

//        [Test]
//        public void ShouldGenerate_AllowNull_WhenNamedAllowNullProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2(null);
//            return 0;
//        }
//    }

//    [StrongType(allow: Allow.Null)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }

//        [Test]
//        public void ShouldGenerate_AllowNull_WhenTypeAndAllowNullProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2((System.Uri)null);
//            return 0;
//        }
//    }

//    [StrongType(typeof(System.Uri), Allow.Null)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }

//        [Test]
//        public void ShouldGenerate_AllowNull_WhenAllowNullEmptyProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2(null);
//            return 0;
//        }
//    }

//    [StrongType(Allow.Null | Allow.Empty)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }

        [Test]
        public void ShouldGenerate_AllowEmpty_WhenAllowEmptyProvided()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType(Allow.Empty)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

            var compiledBytes = CompileBytes(outputCompilation);

            var result = LoadAndExecute(compiledBytes);

            // Assert

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGenerate_AllowEmpty_WhenDoubleAllowEmptyProvided()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType(Allow.Empty | Allow.Empty)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


            var compiledBytes = CompileBytes(outputCompilation);

            var result = LoadAndExecute(compiledBytes);

            // Assert

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGenerate_AllowEmpty_WhenTripleAllowEmptyProvided()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType(Allow.Empty | Allow.Empty | Allow.Empty)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


            var compiledBytes = CompileBytes(outputCompilation);

            var result = LoadAndExecute(compiledBytes);

            // Assert

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGenerate_AllowEmpty_WhenNamedEmptyProvided()
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
            var city = new City2(string.Empty);
            return 0;
        }
    }

    [StrongType(allow: Allow.Empty)]
    public partial class City2
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
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


            var compiledBytes = CompileBytes(outputCompilation);

            var result = LoadAndExecute(compiledBytes);

            // Assert

            Assert.That(result, Is.EqualTo(0));
        }

//        [Test]
//        public void ShouldGenerate_AllowEmpty_WhenTypeAndAllowNullEmptyProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2(string.Empty);
//            return 0;
//        }
//    }

//    [StrongType(typeof(string), Allow.Null | Allow.Empty)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }

//        [Test]
//        public void ShouldGenerate_AllowEmpty_WhenAllowNullEmptyProvided()
//        {
//            //// Arrange

//            var sourceCode = @"
//namespace IntegrationTests.Generated
//{
//    using Xtz.StronglyTyped.SourceGenerator;

//    public class Program
//    {
//        public static int Main(string[] args)
//        {
//            var city = new CityUri2(string.Empty);
//            return 0;
//        }
//    }

//    [StrongType(Allow.Null | Allow.Empty)]
//    public partial class CityUri2
//    {
//    }
//}
//            ";

//            var inputCompilation = CreateCompilation(sourceCode, OutputKind.ConsoleApplication);

//            //// Act

//            // directly create an instance of the generator
//            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
//            var generator = new StronglyTypedGenerator();

//            // Create the driver that will control the generation, passing in our generator
//            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//            // NOTE: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls
//            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);


//            var compiledBytes = CompileBytes(outputCompilation);

//            var result = LoadAndExecute(compiledBytes);

//            // Assert

//            Assert.AreEqual(0, result);
//        }
    }
}
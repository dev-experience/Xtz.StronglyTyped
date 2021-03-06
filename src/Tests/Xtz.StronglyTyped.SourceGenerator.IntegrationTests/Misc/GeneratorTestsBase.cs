using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    public class GeneratorTestsBase
    {
        protected static Compilation CreateCompilation(string source, OutputKind outputKind)
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
                typeof(IStronglyTyped).Assembly,
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

        protected static byte[] CompileBytes(Compilation compilation)
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

        [ExcludeFromCodeCoverage]
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected static object LoadAndExecute(byte[] compiledAssembly, params string[] args)
        {
            using var assemblyStream = new MemoryStream(compiledAssembly);
            using var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

            var assembly = assemblyLoadContext.LoadFromStream(assemblyStream);

            var entryPoint = assembly.EntryPoint;
            if (entryPoint == null)
            {
                throw new GeneratorTestsException("Entry point is not found");
            }

            try
            {
                var result = entryPoint.GetParameters().Length > 0
                    ? entryPoint.Invoke(null, new object[] {args})
                    : entryPoint.Invoke(null, null);
                return result;
            }
            catch (Exception e)
            {
                throw new TestsExecutionException("Failed to run compiled code", e.InnerException);
            }
        }

        protected static void AssertGenerationSuccess(int treeNumber, ImmutableArray<Diagnostic> diagnostics, Compilation outputCompilation,
            GeneratorDriverRunResult runResult)
        {
            Assert.IsTrue(diagnostics.IsEmpty);
            // Input syntax tree, a generated one, and logs
            Assert.AreEqual(treeNumber + 1, outputCompilation.SyntaxTrees.Count());

            PrintDiagnosticsToDebug(outputCompilation);
            Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty);

            // A generated syntax tree and logs
            Assert.AreEqual(treeNumber, runResult.GeneratedTrees.Length);
            Assert.IsTrue(runResult.Diagnostics.IsEmpty);

            // Asserting the individual results on a by-generator basis
            var generatorResult = runResult.Results[0];
            Assert.AreEqual(typeof(StronglyTypedGenerator), generatorResult.Generator.GetType());
            Assert.IsTrue(generatorResult.Diagnostics.IsEmpty);
            Assert.AreEqual(treeNumber, generatorResult.GeneratedSources.Length);
            Assert.IsNull(generatorResult.Exception);
        }

        [ExcludeFromCodeCoverage]
        protected static void PrintDiagnosticsToDebug(Compilation outputCompilation)
        {
            var diagnosticsMessages = outputCompilation.GetDiagnostics().Select(x => x.ToString()).ToArray();
            Debug.WriteLine(string.Join("\n", diagnosticsMessages));
        }
    }
}
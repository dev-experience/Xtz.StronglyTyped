using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [Generator]
    public class StronglyTypedGenerator : IStronglyTypedGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Create a provider for syntax trees
            var syntaxProvider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetTargetForGeneration(ctx))
                .Where(static m => m is not null)
                .Select(static (m, _) => m!.Value);

            // Generate the source
            context.RegisterSourceOutput(syntaxProvider, static (spc, source) => Execute(source, spc));
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            return node is TypeDeclarationSyntax typeDeclarationSyntax &&
                   HasStrongTypeAttribute(typeDeclarationSyntax);
        }

        private static (StrongTypeDeclaration Declaration, Compilation Compilation, SemanticModel SemanticModel)? GetTargetForGeneration(GeneratorSyntaxContext context)
        {
            if (context.Node is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                var semanticModel = context.SemanticModel;
                var compilation = semanticModel.Compilation;
                var declaration = new StrongTypeDeclaration(typeDeclarationSyntax);
                return (declaration, compilation, semanticModel);
            }
            return null;
        }

        private static bool HasStrongTypeAttribute(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax.AttributeLists
                .SelectMany(attrList => attrList.Attributes)
                .Any(attr => IsStrongTypeAttribute(attr));
        }

        private static bool IsStrongTypeAttribute(AttributeSyntax attributeSyntax)
        {
            var name = attributeSyntax.Name.ToString();
            return name == "StrongType" || name.EndsWith(".StrongType");
        }

        private static void Execute((StrongTypeDeclaration Declaration, Compilation Compilation, SemanticModel SemanticModel) data, SourceProductionContext context)
        {
            try
            {
                var now = DateTime.UtcNow;
                var semanticModel = data.SemanticModel;
                
                var dataExtractor = new DataExtractor();
                if (dataExtractor.BuildWorkItem(semanticModel, data.Declaration, out var workItem))
                {
                    if (workItem is null)
                    {
                        context.AddSource("_error_null_workitem", SourceText.From($"// Error: Work item is <null>. Syntax '{data.Declaration.TypeDeclarationSyntax.Identifier}'", Encoding.UTF8));
                        return;
                    }

                    if (workItem.Namespace is null)
                    {
                        context.AddSource("_error_null_namespace", SourceText.From($"// Error: Work namespace is <null>. Syntax '{data.Declaration.TypeDeclarationSyntax.Identifier}'", Encoding.UTF8));
                        return;
                    }

                    var fileName = $"{workItem.Namespace}.{workItem.TypeName}.cs";
                    var generatedSourceCode = GenerateSourceCode(workItem, now);
                    context.AddSource(fileName, SourceText.From(generatedSourceCode, Encoding.UTF8));
                }
            }
            catch (Exception e)
            {
                context.AddSource("_error_exception", SourceText.From($"// Error: {e.Message}\n// Stack trace: {e.StackTrace}", Encoding.UTF8));
            }
        }

        private static string GenerateSourceCode(StronglyTypedWorkItem workItem, DateTime timestamp)
        {
            var writer = new CodeWriter();

            WriteBanner(writer, workItem, timestamp);

            writer.AppendLine();
            using (writer.BeginScope($"namespace {workItem.Namespace}"))
            {
                writer.AppendLine("using System.ComponentModel;");
                writer.AppendLine("using System.Text.Json.Serialization;");
                writer.AppendLine();

                WriteTypeConverter(writer, workItem);
                writer.AppendLine($"[JsonConverter(typeof(Xtz.StronglyTyped.TypeConverters.StronglyTypedJsonConverter<{workItem.Namespace}.{workItem.TypeName}>))]");

                switch (workItem.Kind)
                {
                    case WorkItemKind.Class:
                        WriteClass(writer, workItem);
                        break;
                    case WorkItemKind.Struct:
                        WriteStruct(writer, workItem);
                        break;
                    case WorkItemKind.Unknown:
                    default:
                        throw new CodeWriterException($"Not supported work item type '{workItem.Kind}'");
                }
            }

            var generatedSourceCode = writer.ToString();
            return generatedSourceCode;
        }

        private static void WriteBanner(CodeWriter writer, StronglyTypedWorkItem workItem, DateTime timestamp)
        {
            var version = typeof(StronglyTypedGenerator).Assembly.GetName().Version;
            var assemblyVersion = $"{version!.Major}.{version.Minor}.{version.Revision}.{version.Build}";

            writer.AppendLine(
                $@"//------------------------------------
// <auto-generated>
//     Type `{workItem.Namespace}.{workItem.TypeName}`
//
//     This code was generated by generator '{typeof(StronglyTypedGenerator).FullName}'
//     Assembly Version: {assemblyVersion}
//     Generation timestamp: {timestamp:s}Z
// </auto-generated>
//------------------------------------");
        }

        private static void WriteTypeConverter(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            var valueType = workItem.InnerType;
            var typeConverter = valueType switch
            {
                var t when t == typeof(string) => $"[TypeConverter(typeof(Xtz.StronglyTyped.TypeConverters.StringTypeConverter<{workItem.TypeName}>))]",
                _ => $"[TypeConverter(typeof(Xtz.StronglyTyped.TypeConverters.TypeConverter<{workItem.TypeName}, {workItem.InnerType.FullName}>))]",
            };

            writer.AppendLine(typeConverter);
        }

        private static void WriteClass(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            var baseType = !workItem.ExtraFeatures.HasBaseClass
                ? $" Xtz.StronglyTyped.StronglyTyped<{workItem.InnerType.FullName}>,"
                : String.Empty;

            var sealedStr = workItem.ExtraFeatures.IsAbstract
                ? string.Empty
                : " sealed";

            using (writer.BeginScope($"public{sealedStr} partial class {workItem.TypeName} :{baseType} System.IEquatable<{workItem.TypeName}>"))
            {
                WriteXmlSummary(writer, $"Initializes a new instance of the <see cref=\"{workItem.TypeName}\"/> class.");
                WriteXmlParam(writer, "value", "Inner value");
                writer.AppendLine($"public {workItem.TypeName}({workItem.InnerType.FullName} value)");
                writer.AppendLine("    : base(value)");
                using (writer.BeginScope())
                {
                }
                writer.AppendLine();

                TryWriteCustomConstructors(writer, workItem);

                TryWriteAllowEmpty(writer, workItem);

                TryWriteToString(writer, workItem);

                WriteEquatableEquals(writer, workItem);
                writer.AppendLine();

                WriteExplicitOperatorToStrongType(writer, workItem);
                writer.AppendLine();

                WriteImplicitOperatorsFromStrongType(writer, workItem);
            }
        }

        private static void TryWriteAllowEmpty(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (workItem.ExtraFeatures.DoesAllowEmpty)
            {
                WriteXmlSummary(writer, "Will throw if empty inner value provided");
                writer.AppendLine("protected override bool ShouldThrowIfEmpty() => false;");
                writer.AppendLine();
            }
        }

        private static void WriteStruct(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            writer.AppendLine("[System.Diagnostics.DebuggerDisplay(\"[struct {typeof(StronglyTypedGenerator).Name,nq}] {Value}\")]");
            using (writer.BeginScope($"public readonly partial struct {workItem.TypeName} : Xtz.StronglyTyped.IStronglyTyped<{workItem.InnerType.FullName}>, System.IEquatable<{workItem.TypeName}>"))
            {
                WriteXmlSummary(writer, "Default instance.");
                writer.AppendLine($"public static readonly {workItem.TypeName} Default;");
                writer.AppendLine();

                WriteXmlSummary(writer, $"Inner value.");
                writer.AppendLine($"public {workItem.InnerType.FullName} Value {{ get; }}");
                writer.AppendLine();

                WriteXmlSummary(writer, $"Initializes a new instance of the <see cref=\"{workItem.TypeName}\"/> struct.");
                WriteXmlParam(writer, "value", "Inner value");
                writer.AppendLine($"public {workItem.TypeName}({workItem.InnerType.FullName} value)");
                using (writer.BeginScope())
                {
                    writer.AppendLine("Value = value;");
                    writer.AppendLine("ThrowIfInvalid(value);");
                }
                writer.AppendLine();

                TryWriteCustomConstructors(writer, workItem);

                WriteStructThrowIfInvalid(writer, workItem);

                TryWriteToString(writer, workItem);

                WriteEquatableEquals(writer, workItem);
                writer.AppendLine();

                WriteStructEqualityMethods(writer, workItem);
                writer.AppendLine();

                WriteExplicitOperatorToStrongType(writer, workItem);
                writer.AppendLine();

                WriteImplicitOperatorsFromStrongType(writer, workItem);
            }
        }

        private static void TryWriteCustomConstructors(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (workItem.InnerType == typeof(Guid))
            {
                WriteGuidStringConstructor(writer, workItem);
            }

            if (workItem.ExtraFeatures.DoGenerateStringConstructor)
            {
                WriteStringConstructor(writer, workItem);
            }
        }

        private static void WriteGuidStringConstructor(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            var typeKindStr = workItem.Kind == WorkItemKind.Struct
                ? "struct"
                : "class";

            if (workItem.Kind == WorkItemKind.Class)
            {
                WriteXmlSummary(writer,
                    $"Initializes a new instance of the <see cref=\"{workItem.TypeName}\"/> {typeKindStr}.");
                writer.AppendLine($"public {workItem.TypeName}()");
                writer.AppendLine("    : this(System.Guid.NewGuid())");

                using (writer.BeginScope())
                {
                }

                writer.AppendLine();
            }
        }

        private static void WriteStringConstructor(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            WriteXmlSummary(writer, $"Initializes a new instance of the <see cref=\"{workItem.TypeName}\"/> class.");
            WriteXmlParam(writer, "value", "String value to convert");
            writer.AppendLine($"public {workItem.TypeName}(string value)");
            writer.AppendLine($"    : this(new {workItem.InnerType.FullName}(value))");
            using (writer.BeginScope())
            {
            }
            writer.AppendLine();
        }



        private static void WriteStructThrowIfInvalid(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            using (writer.BeginScope($"private void ThrowIfInvalid({workItem.InnerType.FullName} value)"))
            {
                if (workItem.InnerType.IsClass)
                {
                    using (writer.BeginScope("if (value == null)"))
                    {
                        writer.AppendLine("Throw($\"<null> value is invalid for type {typeof(StronglyTypedGenerator)}\");");
                    }
                }

                if (workItem.InnerType == typeof (string))
                {
                    writer.AppendLine();
                    using (writer.BeginScope("if (value == string.Empty)"))
                    {
                        writer.AppendLine("Throw($\"'' value is invalid for type {typeof(StronglyTypedGenerator)}\");");
                    }
                }

                if (workItem.ExtraFeatures.HasIsValid)
                {
                    writer.AppendLine();
                    using (writer.BeginScope("if (!IsValid(value))"))
                    {
                        writer.AppendLine("Throw($\"'{value}' value is invalid for type {typeof(StronglyTypedGenerator)}\");");
                    }
                }
            }
            writer.AppendLine();

            writer.AppendLine("private void Throw(string errorMessage) => throw new Xtz.StronglyTyped.StronglyTypedException(typeof(StronglyTypedGenerator), errorMessage);");
            writer.AppendLine();
        }

        private static void WriteStructEqualityMethods(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            WriteXmlSummary(writer, "Determines whether the specified object is equal to the current struct.");
            WriteXmlParam(writer, "obj", "The object to compare with the current struct.");
            WriteXmlReturns(writer, "<see langword=\"true\" /> if the specified object  is equal to the current object; otherwise, <see langword=\"false\" />.");
            using (writer.BeginScope("public override bool Equals(object obj)"))
            {
                writer.AppendLine($"return obj is {workItem.TypeName} other && Equals(other);");
            }
            writer.AppendLine();

            WriteXmlSummary(writer, "Returns the hash code for this instance (hash code of the inner value).");
            WriteXmlReturns(writer, "A 32-bit signed integer that is the hash code for this instance.");
            using (writer.BeginScope("public override int GetHashCode()"))
            {
                if (workItem.InnerType.IsValueType)
                {
                    writer.AppendLine("return Value.GetHashCode();");
                }
                else
                {
                    writer.AppendLine("return Value?.GetHashCode() ?? default;");
                }
            }
        }

        private static void WriteEquatableEquals(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            WriteXmlSummary(writer, "Determines whether the specified object is equal to the current instance.");
            WriteXmlParam(writer, "other", "The object to compare with the current instance.");
            WriteXmlReturns(writer, "<see langword=\"true\" /> if the specified object is equal to the current instance; otherwise, <see langword=\"false\" />.");
            using (writer.BeginScope($"public bool Equals({workItem.TypeName} other)"))
            {
                writer.AppendLine("if (ReferenceEquals(null, other)) return false;");
                writer.AppendLine("if (ReferenceEquals(this, other)) return true;");
                writer.AppendLine("return Equals(Value, other.Value);");
            }
        }

        private static void TryWriteToString(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (workItem.Kind == WorkItemKind.Struct && !workItem.ExtraFeatures.HasToString)
            {
                WriteXmlSummary(writer, "Returns a string that represents inner value.");
                WriteXmlReturns(writer, "A string that represents inner value.");
                using (writer.BeginScope("public override string ToString()"))
                {
                    if (workItem.InnerType == typeof(Guid))
                    {
                        writer.AppendLine("return $\"{Value:D}\";");
                    }
                    else
                    {
                        writer.AppendLine("return $\"{Value}\";");
                    }
                }
                writer.AppendLine();
                return;
            }

            if (workItem.Kind == WorkItemKind.Class && !workItem.ExtraFeatures.HasToString && workItem.InnerType == typeof(Guid))
            {
                WriteXmlSummary(writer, "Returns a string that represents inner <see cref=\"System.Guid\"/>.");
                WriteXmlReturns(writer, "A string that represents inner <see cref=\"System.Guid\"/>.");
                using (writer.BeginScope("public override string ToString()"))
                {
                    writer.AppendLine("return $\"{Value:D}\";");
                }
                writer.AppendLine();
                return;
            }
        }

        /// <summary>
        /// <see cref="String"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="workItem"></param>
        private static void WriteExplicitOperatorToStrongType(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            WriteXmlSummary(writer, $"Explicitly converts <see cref=\"{workItem.InnerType.FullName}\"/> to <see cref=\"{workItem.TypeName}\"/>.");
            WriteXmlParam(writer, "value", "A value to convert from.");
            WriteXmlReturns(writer, $"A converted <see cref=\"{workItem.TypeName}\"/> value.");
            using (writer.BeginScope(
                $"public static explicit operator {workItem.TypeName}({workItem.InnerType.FullName} value)"))
            {
                writer.AppendLine($"return new {workItem.TypeName}(value);");
            }

            if (workItem.InnerType != typeof(string))
            {
                writer.AppendLine();
                WriteXmlSummary(writer,
                    $"Explicitly converts <see cref=\"string\"/> to <see cref=\"{workItem.TypeName}\"/>.");
                WriteXmlParam(writer, "value", "A value to convert from.");
                WriteXmlReturns(writer, $"A converted <see cref=\"{workItem.TypeName}\"/> value.");
                using (writer.BeginScope(
                    $"public static explicit operator {workItem.TypeName}(string value)"))
                {
                    writer.AppendLine($"return new {workItem.TypeName}(value);");
                }
            }
        }

        private static void WriteImplicitOperatorsFromStrongType(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            WriteXmlSummary(writer, $"Implicitly converts <see cref=\"{workItem.TypeName}\"/> to <see cref=\"{workItem.InnerType.FullName}\"/>.");
            WriteXmlParam(writer, "stronglyTyped", "A value to convert from.");
            WriteXmlReturns(writer, $"A converted <see cref=\"{workItem.InnerType.FullName}\"/> value.");
            using (writer.BeginScope(
                $"public static implicit operator {workItem.InnerType.FullName}({workItem.TypeName} stronglyTyped)"))
            {
                if (workItem.Kind == WorkItemKind.Class)
                {
                    writer.AppendLine($"return stronglyTyped?.Value ?? default({workItem.InnerType.FullName});");
                }
                else
                {
                    writer.AppendLine("return stronglyTyped.Value;");
                }
            }

            if (workItem.InnerType != typeof(string))
            {
                writer.AppendLine();
                WriteXmlSummary(writer, $"Implicitly converts <see cref=\"{workItem.TypeName}\"/> to <see cref=\"string\"/>.");
                WriteXmlParam(writer, "stronglyTyped", "A value to convert from.");
                WriteXmlReturns(writer, "A converted <see cref=\"string\"/> value.");
                using (writer.BeginScope(
                    $"public static implicit operator string({workItem.TypeName} stronglyTyped)"))
                {
                    if (workItem.Kind == WorkItemKind.Class)
                    {
                        writer.AppendLine("return stronglyTyped?.ToString() ?? string.Empty;");
                    }
                    else
                    {
                        writer.AppendLine("return stronglyTyped.ToString();");
                    }
                }
            }
        }

        private static void WriteXmlSummary(CodeWriter writer, string summary)
        {
            writer.AppendLine($"/// <summary>{summary}</summary>");
        }

        private static void WriteXmlReturns(CodeWriter writer, string description)
        {
            writer.AppendLine($"/// <returns>{description}</returns>");
        }

        private static void WriteXmlParam(CodeWriter writer, string paramName, string description)
        {
            writer.AppendLine($"/// <param name=\"{paramName}\">{description}</param>");
        }


    }
}
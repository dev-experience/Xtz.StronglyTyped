using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [Generator]
    public class StronglyTypedGenerator : IStronglyTypedGenerator
    {
        private static readonly Dictionary<Type, ConstructorDescriptor> KNOWN_CONSTRUCTORS = new()
        {
            { typeof(bool), new("bool", "bool.Parse(value)") },
            { typeof(byte), new("byte", "byte.Parse(value)") },
            { typeof(char), new("char", "char.Parse(value)") },
            { typeof(decimal), new("decimal", "decimal.Parse(value)") },
            { typeof(double), new("double", "double.Parse(value)") },
            { typeof(float), new("float", "float.Parse(value)") },
            { typeof(int), new("int", "int.Parse(value)") },
            { typeof(long), new("long", "long.Parse(value)") },
            { typeof(sbyte), new("sbyte", "sbyte.Parse(value)") },
            { typeof(short), new("short", "short.Parse(value)") },
            { typeof(uint), new("uint", "uint.Parse(value)") },
            { typeof(ulong), new("ulong", "ulong.Parse(value)") },
            { typeof(ushort), new("ushort", "ushort.Parse(value)") },
            { typeof(DateTime), new(typeof(DateTime).FullName, "System.DateTime.Parse(value)") },
            { typeof(TimeSpan), new(typeof(TimeSpan).FullName, "System.TimeSpan.Parse(value)") },
            { typeof(Guid), new(typeof(Guid).FullName, "System.Guid.Parse(value)") },
            // Skipping `MailAddress` as it has `(string value)` constructor
            { typeof(IPAddress), new(typeof(IPAddress).FullName, "System.Net.IPAddress.Parse(value)") },
            { typeof(PhysicalAddress), new(typeof(PhysicalAddress).FullName, "System.Net.NetworkInformation.PhysicalAddress.Parse(value)") },
            // Skipping `Uri` as it has `(string value)` constructor
        };
        
        private readonly IDataExtractor _dataExtractor = new DataExtractor();

        private readonly List<string> _log = new();

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver receiver) return;

            try
            {
                var now = DateTime.UtcNow;

                foreach (var declaration in receiver.Declarations)
                {
                    try
                    {
                        var semanticModel = context.Compilation.GetSemanticModel(declaration.TypeDeclarationSyntax.SyntaxTree, true);
                        if (_dataExtractor.BuildWorkItem(semanticModel, receiver, declaration, out var workItem))
                        {
                            if (workItem is null)
                            {
                                _log.Add($"Error: Work item is <null>. Syntax '{declaration.TypeDeclarationSyntax.Identifier}'");
                                continue;
                            }

                            if (workItem.Namespace is null)
                            {
                                _log.Add($"Error: Work namespace is <null>. Syntax '{declaration.TypeDeclarationSyntax.Identifier}'");
                                continue;
                            }

                            var fileName = $"{workItem.Namespace}.{workItem.TypeName}.cs";
                            var generatedSourceCode = GenerateSourceCode(workItem, now);
                            context.AddSource(fileName, SourceText.From(generatedSourceCode, Encoding.UTF8));
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        var process = Process.GetCurrentProcess();
                        var processId = process.Id;
                        var processName = process.ProcessName;
#if DEBUG
                        if (!Debugger.IsAttached) Debugger.Launch();
#endif
                        // IMPORTANT: Check if you use any types from `Xtz.StronglyTyped` library or any other library. Remove if any. Dependencies are not copied along with source generator (if they are not analyzers as well).
                        // https://github.com/dotnet/roslyn/discussions/47517#discussioncomment-63842
                        
                        _log.Add("\nIMPORTANT: Check if you use any types from `Xtz.StronglyTyped` library or any other library. Remove if any. Dependencies are not copied along with source generator (if they are not analyzers as well).\nhttps://github.com/dotnet/roslyn/discussions/47517#discussioncomment-63842\n");
                        _log.Add($"Method '{nameof(StronglyTypedGenerator)}.{nameof(Execute)}()' threw an exception '{e.Message}'.\n\nStack trace: {e.StackTrace}\n\nProcess ID: {processId}\n\nProcess name: {processName}\n\nFusion log: {e.FusionLog}");
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        if (!Debugger.IsAttached) Debugger.Launch();
#endif

                        _log.Add($"Method '{nameof(StronglyTypedGenerator)}.{nameof(Execute)}()' threw an exception '{e.Message}'.\nStack trace: {e.StackTrace}");
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                if (!Debugger.IsAttached) Debugger.Launch();
#endif

                _log.Add($"Method '{nameof(StronglyTypedGenerator)}.{nameof(Execute)}()' threw an exception '{e.Message}'.\nStack trace: {e.StackTrace}");
            }
            finally
            {
                context.AddSource("_1-receiver-log", BuildLogText(receiver.Log, "SYNTAX RECEIVER LOG"));
                context.AddSource("_2-data-extractor-log", BuildLogText(_dataExtractor.Log, "DATA EXTRACTOR LOG"));
                context.AddSource($"_3-generator-log", BuildLogText(_log, "GENERATOR LOG"));
            }
        }

        private string GenerateSourceCode(StronglyTypedWorkItem workItem, DateTime timestamp)
        {
            var writer = new CodeWriter();

            var version = GetType().Assembly.GetName().Version;
            var assemblyVersion = $"{version.Major}.{version.Minor}.{version.Revision}.{version.Build}";

            WriteBanner(writer, workItem, assemblyVersion, timestamp);

            writer.AppendLine();
            using (writer.BeginScope($"namespace {workItem.Namespace}"))
            {
                writer.AppendLine("using System.ComponentModel;");
                writer.AppendLine("using System.Text.Json.Serialization;");
                writer.AppendLine();

                writer.AppendLine($"[System.CodeDom.Compiler.GeneratedCode(\"{GetType().FullName}\", \"{assemblyVersion}\")]");
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

        private void WriteBanner(CodeWriter writer, StronglyTypedWorkItem workItem, string assemblyVersion, DateTime timestamp)
        {
            writer.AppendLine(
                $@"//------------------------------------
// <auto-generated>
//     Type `{workItem.Namespace}.{workItem.TypeName}`
//
//     This code was generated by generator '{GetType().FullName}'
//     Assembly Version: {assemblyVersion}
//     Generation timestamp: {timestamp:s}Z
// </auto-generated>
//------------------------------------");
        }

        private void WriteTypeConverter(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            var valueType = workItem.InnerType;
            var typeConverter = valueType switch
            {
                var t when t == typeof(string) => $"[TypeConverter(typeof(Xtz.StronglyTyped.TypeConverters.StringTypeConverter<{workItem.TypeName}>))]",
                _ => $"[TypeConverter(typeof(Xtz.StronglyTyped.TypeConverters.TypeConverter<{workItem.TypeName}, {workItem.InnerType.FullName}>))]",
            };

            writer.AppendLine(typeConverter);
        }

        private void WriteClass(CodeWriter writer, StronglyTypedWorkItem workItem)
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

        private void TryWriteAllowEmpty(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (workItem.ExtraFeatures.DoesAllowEmpty)
            {
                WriteXmlSummary(writer, "Will throw if empty inner value provided");
                writer.AppendLine("protected override bool ShouldThrowIfEmpty() => false;");
                writer.AppendLine();
            }
        }

        private void WriteStruct(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            writer.AppendLine("[System.Diagnostics.DebuggerDisplay(\"[struct {GetType().Name,nq}] {Value}\")]");
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

        private void TryWriteCustomConstructors(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (workItem.InnerType == typeof(Guid))
            {
                WriteGuidStringConstructor(writer, workItem);
            }

            if (workItem.ExtraFeatures.DoGenerateStringConstructor)
            {
                WriteStringConstructor(writer, workItem);
            }
            else
            {
                TryWriteParsingStringConstructor(writer, workItem);
            }
        }

        private void WriteGuidStringConstructor(CodeWriter writer, StronglyTypedWorkItem workItem)
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

        private void WriteStringConstructor(CodeWriter writer, StronglyTypedWorkItem workItem)
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

        private void TryWriteParsingStringConstructor(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            if (KNOWN_CONSTRUCTORS.TryGetValue(workItem.InnerType, out var descriptor))
            {
                WriteXmlSummary(writer, $"Initializes a new instance of the <see cref=\"{workItem.TypeName}\"/> class.");
                WriteXmlParam(writer, "value", "String value to convert");
                writer.AppendLine($"public {workItem.TypeName}(string value)");
                writer.AppendLine($"    : this({descriptor.ParsingExpression})");
                using (writer.BeginScope())
                {
                }
                writer.AppendLine();
            }
        }

        private void WriteStructThrowIfInvalid(CodeWriter writer, StronglyTypedWorkItem workItem)
        {
            using (writer.BeginScope($"private void ThrowIfInvalid({workItem.InnerType.FullName} value)"))
            {
                if (workItem.InnerType.IsClass)
                {
                    using (writer.BeginScope("if (value == null)"))
                    {
                        writer.AppendLine("Throw($\"<null> value is invalid for type {GetType()}\");");
                    }
                }

                if (workItem.InnerType == typeof(string) && !workItem.ExtraFeatures.DoesAllowEmpty)
                {
                    writer.AppendLine();
                    using (writer.BeginScope("if (value == string.Empty)"))
                    {
                        writer.AppendLine("Throw($\"'' value is invalid for type {GetType()}\");");
                    }
                }

                if (workItem.ExtraFeatures.HasIsValid)
                {
                    writer.AppendLine();
                    using (writer.BeginScope("if (!IsValid(value))"))
                    {
                        writer.AppendLine("Throw($\"'{Value}' value is invalid for type {GetType()}\");");
                    }
                }
            }
            writer.AppendLine();

            writer.AppendLine("private void Throw(string errorMessage) => throw new Xtz.StronglyTyped.StronglyTypedException(GetType(), errorMessage);");
            writer.AppendLine();
        }

        private void WriteStructEqualityMethods(CodeWriter writer, StronglyTypedWorkItem workItem)
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
            if (!workItem.ExtraFeatures.HasToString)
            {
                // Order of ifs is important

                if (workItem.InnerType == typeof(Guid))
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

                if (workItem.InnerType == typeof(DateTime))
                {
                    WriteXmlSummary(writer, "Returns an ISO-8601 string that represents inner <see cref=\"System.DateTime\"/>.");
                    WriteXmlReturns(writer, "An ISO-8601 string that represents inner <see cref=\"System.DateTime\"/>.");
                    using (writer.BeginScope("public override string ToString()"))
                    {
                        writer.AppendLine("return $\"{Value.ToUniversalTime():s}Z\";");
                    }
                    writer.AppendLine();
                    return;
                }

                if (workItem.InnerType == typeof(TimeSpan))
                {
                    WriteXmlSummary(writer, "Returns an ISO-8601 string that represents inner <see cref=\"System.TimeSpan\"/>.");
                    WriteXmlReturns(writer, "An ISO-8601 string that represents inner <see cref=\"System.TimeSpan\"/>.");
                    using (writer.BeginScope("public override string ToString()"))
                    {
                        writer.AppendLine("return $\"{System.Xml.XmlConvert.ToString(Value)}\";");
                    }
                    writer.AppendLine();
                    return;
                }

                if (workItem.Kind == WorkItemKind.Struct)
                {
                    WriteXmlSummary(writer, "Returns a string that represents inner value.");
                    WriteXmlReturns(writer, "A string that represents inner value.");
                    using (writer.BeginScope("public override string ToString()"))
                    {
                        writer.AppendLine("return $\"{Value}\";");
                    }
                    writer.AppendLine();
                    return;
                }
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

        private static SourceText BuildLogText(IReadOnlyCollection<string> log, string title)
        {
            var version = typeof(StronglyTypedGenerator).Assembly.GetName().Version;
            var assemblyVersion = $"{version.Major}.{version.Minor}.{version.Revision}.{version.Build}";
 
            var result = SourceText.From(
                string.Format(@"/*{0}{1}{0}{0}{2}{0}{0}{3}{0}{0}{4}{0}{0}*/",
                    Environment.NewLine,
                    title,
                    $"This code was generated by generator '{typeof(StronglyTypedGenerator).FullName}'\nAssembly Version: {assemblyVersion}",
                    $"{DateTime.UtcNow:s}Z",
                    string.Join(Environment.NewLine, log)),
                Encoding.UTF8);
            return result;
        }
    }
}
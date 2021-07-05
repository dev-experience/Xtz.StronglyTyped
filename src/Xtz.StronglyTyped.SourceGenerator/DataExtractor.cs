using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public class DataExtractor : IDataExtractor
    {
        private static readonly Assembly SYSTEM_PRIVATE_CORE_LIB_ASSEMBLY = Assembly.GetAssembly(typeof(string));

        private static readonly Dictionary<string, Type> KNOWN_TYPES = new()
        {
            { "bool", typeof(bool) },
            { "byte", typeof(byte) },
            { "char", typeof(char) },
            { "decimal", typeof(decimal) },
            { "double", typeof(double) },
            { "float", typeof(float) },
            { "int", typeof(int) },
            { "long", typeof(long) },
            { "sbyte", typeof(sbyte) },
            { "short", typeof(short) },
            { "string", typeof(string) },
            { "uint", typeof(uint) },
            { "ulong", typeof(ulong) },
            { "ushort", typeof(ushort) },
            { typeof(Guid).FullName, typeof(Guid) },
            { typeof(Uri).FullName, typeof(Uri) },
            { typeof(MailAddress).FullName, typeof(MailAddress) },
            { typeof(IPAddress).FullName, typeof(IPAddress) },
            { typeof(PhysicalAddress).FullName, typeof(PhysicalAddress) },
        };

        private static readonly Type DEFAULT_INNER_TYPE = typeof(string);

        private readonly List<string> _log = new();

        public IReadOnlyCollection<string> Log => _log;

        public bool BuildWorkItem(
            SemanticModel semanticModel,
            SyntaxReceiver receiver,
            StrongTypeDeclaration declaration,
            out StronglyTypedWorkItem? workItem)
        {
            workItem = null;

            var typeDeclarationSyntax = declaration.TypeDeclarationSyntax;

            var originalTypeName = typeDeclarationSyntax.Identifier.ValueText;
            var itemKind = ExtractItemKind(typeDeclarationSyntax);
            if (itemKind == WorkItemKind.Unknown)
            {
                _log.Add($"Unknown kind '{itemKind}' for type '{originalTypeName}'. Ignoring");
                return false;
            }

            var originalNamespace = ExtractNamespace(semanticModel, typeDeclarationSyntax);

            var innerType = ExtractInnerType(semanticModel, receiver, typeDeclarationSyntax);
            if (innerType == null)
            {
                _log.Add($"Unable to identify inner type for type '{originalTypeName}'. Ignoring");
                return false;
            }

            var extraFeatures = ExtractExtraFeatures(semanticModel, typeDeclarationSyntax, innerType);

            _log.Add($"Found a type ({itemKind}) '{originalNamespace}.{originalTypeName}'");
            _log.Add($"Inner type '{innerType}'");
            _log.Add($"{extraFeatures}");
            _log.Add(string.Empty);

            workItem = new StronglyTypedWorkItem(declaration, itemKind, originalNamespace, originalTypeName, innerType, extraFeatures);
            return true;
        }

        public WorkItemKind ExtractItemKind(SyntaxNode syntaxNode)
        {
            var itemKind = syntaxNode switch
            {
                ClassDeclarationSyntax => WorkItemKind.Class,
                StructDeclarationSyntax => WorkItemKind.Struct,
                _ => WorkItemKind.Unknown,
            };
            return itemKind;
        }

        public Type? ExtractInnerType(
            SemanticModel semanticModel,
            SyntaxReceiver receiver,
            TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var attributeSyntax = typeDeclarationSyntax.AttributeLists
                .SelectMany(x => x.Attributes)
                .FirstOrDefault(x => receiver.IsStrongTypeAttribute(x));

            if (attributeSyntax != null)
            {
                var innerType = ExtractInnerType(semanticModel, attributeSyntax);
                return innerType;
            }

            // Never happens
            return null;
        }

        private Type? ExtractInnerType(SemanticModel semanticModel, AttributeSyntax attributeSyntax)
        {
            var firstArgument = attributeSyntax.ArgumentList?.Arguments.FirstOrDefault();
            if (firstArgument is null) return DEFAULT_INNER_TYPE;

            if (firstArgument.Expression is not TypeOfExpressionSyntax typeOfExpressionSyntax) return DEFAULT_INNER_TYPE;

            var typeSyntax = typeOfExpressionSyntax.Type;

            var typeSymbolInfo = semanticModel.GetTypeInfo(typeSyntax);
            var typeSymbol = typeSymbolInfo.Type;
            if (typeSymbol == null) return DEFAULT_INNER_TYPE;

            var typeName = typeSymbol.ToDisplayString();
            var result = GetTypeByName(typeName);
            return result;
        }

        private bool HasBaseClass(SemanticModel semanticModel, TypeDeclarationSyntax typeDeclarationSyntax)
        {
            if (typeDeclarationSyntax.BaseList?.Types is null) return false;

            foreach (var baseTypeSyntax in typeDeclarationSyntax.BaseList.Types)
            {
                var typeKind = semanticModel.GetTypeInfo(baseTypeSyntax.Type).Type?.TypeKind;
                if (typeKind == TypeKind.Class)
                {
                    return true;
                }
            }

            return false;
        }

        private static Type? GetTypeByName(string typeName)
        {
            if (KNOWN_TYPES.TryGetValue(typeName, out var knownType))
            {
                return knownType;
            }

            var systemType = SYSTEM_PRIVATE_CORE_LIB_ASSEMBLY.GetType(typeName);
            if (systemType != null)
            {
                return systemType;
            }

            return null;
        }

        private string? ExtractNamespace(SemanticModel semanticModel, TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var namespaceDeclarationSyntax = typeDeclarationSyntax.FirstAncestorOrSelf<NamespaceDeclarationSyntax>(_ => true);
            if (namespaceDeclarationSyntax is null) return null;

            var typeSymbolInfo = semanticModel.GetDeclaredSymbol(namespaceDeclarationSyntax) as INamespaceSymbol;
            var result = typeSymbolInfo?.ToDisplayString();
            return result;
        }

        private ExtraFeatures ExtractExtraFeatures(
            SemanticModel semanticModel,
            TypeDeclarationSyntax typeDeclarationSyntax,
            Type innerType)
        {
            var isAbstract = typeDeclarationSyntax.Modifiers.Any(x => Equals(x.Value, "abstract"));

            var hasBaseType = HasBaseClass(semanticModel, typeDeclarationSyntax);

            var innerTypeHasStringConstructor = innerType.GetConstructor(new[] { typeof(string) }) != null;

            var hasStringConstructor = false;
            if (innerTypeHasStringConstructor && typeDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                var constructorDeclarationSyntaxes = classDeclarationSyntax.Members
                    .Where(x => x is ConstructorDeclarationSyntax)
                    .Cast<ConstructorDeclarationSyntax>();
                if (constructorDeclarationSyntaxes.Any(x => HasSingleParameter(semanticModel, x, "string")))
                {
                    hasStringConstructor = true;
                }
            }

            var doGenerateStringConstructor = innerType != typeof(string) && innerTypeHasStringConstructor && !hasStringConstructor;

            var hasToString = false;
            var methodDeclarationSyntaxes = typeDeclarationSyntax.Members
                .Where(x => x is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
                .ToArray();
            if (methodDeclarationSyntaxes.Any(x => x.Identifier.Text == "ToString" && x.ParameterList.Parameters.Count == 0))
            {
                hasToString = true;
            }

            var hasIsValid = false;
            if (methodDeclarationSyntaxes.Any(x => HasSingleParameter(semanticModel, x, innerType.FullName!)))
            {
                hasIsValid = true;
            }

            return new ExtraFeatures(isAbstract, hasBaseType, doGenerateStringConstructor, hasToString, hasIsValid);
        }

        private static bool HasSingleParameter(SemanticModel semanticModel, BaseMethodDeclarationSyntax x, string expectedParameterType)
        {
            if (x.ParameterList.Parameters.Count != 1) return false;

            var typeSyntax = x.ParameterList.Parameters.First().Type;
            var symbolInfo = semanticModel.GetTypeInfo(typeSyntax!);
            var result = symbolInfo.Type?.ToDisplayString() == expectedParameterType;
            return result;
        }
    }
}
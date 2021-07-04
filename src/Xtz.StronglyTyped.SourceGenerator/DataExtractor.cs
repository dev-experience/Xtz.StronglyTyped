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
            var workItemType = ExtractWorkItemType(typeDeclarationSyntax);
            _log.Add($"Found a type ({workItemType}) '{originalTypeName}'");
            if (workItemType == WorkItemKind.Unknown) return false;

            var hasBaseType = HasBaseClass(semanticModel, typeDeclarationSyntax);

            var originalNamespace = ExtractNamespace(semanticModel, typeDeclarationSyntax);
            _log.Add($"Identified a namespace '{originalNamespace}' for type ({workItemType}) '{originalTypeName}'");

            var innerType = ExtractInnerType(semanticModel, receiver, typeDeclarationSyntax);
            if (innerType == null) return false;

            _log.Add($"Identified inner type '{innerType}' for type ({workItemType}) '{originalTypeName}'");

            var extraFeatures = ExtractExtraFeatures(semanticModel, receiver, typeDeclarationSyntax, innerType);

            _log.Add(string.Empty);

            workItem = new StronglyTypedWorkItem(declaration, workItemType, originalNamespace, originalTypeName, innerType, hasBaseType, extraFeatures);
            return true;
        }

        public WorkItemKind ExtractWorkItemType(SyntaxNode syntaxNode)
        {
            var itemType = syntaxNode switch
            {
                ClassDeclarationSyntax => WorkItemKind.Class,
                StructDeclarationSyntax => WorkItemKind.Struct,
                _ => WorkItemKind.Unknown,
            };
            return itemType;
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

        private string ExtractNamespace(SemanticModel semanticModel, TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var namespaceDeclarationSyntax = typeDeclarationSyntax.FirstAncestorOrSelf<NamespaceDeclarationSyntax>(x => true);
            var typeSymbolInfo = semanticModel.GetDeclaredSymbol(namespaceDeclarationSyntax!) as INamespaceSymbol;
            if (typeSymbolInfo is null) throw new SyntaxReceiverException($"Failed to find the namespace for '{typeDeclarationSyntax.Identifier.ValueText}'");

            var result = typeSymbolInfo.ToDisplayString();
            return result;
        }

        private ExtraFeatures ExtractExtraFeatures(
            SemanticModel semanticModel,
            SyntaxReceiver receiver,
            TypeDeclarationSyntax typeDeclarationSyntax,
            Type innerType)
        {
            var isAbstract = typeDeclarationSyntax.Modifiers.Any(x => Equals(x.Value, "abstract"));

            var hasStringConstructor = false;
            if (typeDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                var constructorDeclarationSyntaxes = classDeclarationSyntax.Members
                    .Where(x => x is ConstructorDeclarationSyntax)
                    .Cast<ConstructorDeclarationSyntax>();
                if (constructorDeclarationSyntaxes.Any(x => x.ParameterList.Parameters.Count == 1 && IsString(semanticModel, x)))
                {
                    hasStringConstructor = true;
                }
            }

            var doGenerateStringConstructor = innerType.GetConstructor(new[] {typeof(string)}) != null;

            var isStruct = typeDeclarationSyntax is StructDeclarationSyntax;
            if (isStruct)
            {
                // IsValid
            }

            return new ExtraFeatures(isAbstract, hasStringConstructor, doGenerateStringConstructor);
        }

        private static bool IsString(SemanticModel semanticModel, ConstructorDeclarationSyntax x)
        {
            var typeSyntax = x.ParameterList.Parameters.FirstOrDefault()?.Type;
            if (typeSyntax is null) return false;

            var symbolInfo = semanticModel.GetTypeInfo(typeSyntax);
            var result = symbolInfo.Type?.ToDisplayString() == "string";
            return result;
        }
    }
}
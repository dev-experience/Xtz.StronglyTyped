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
            { typeof(DateTime).FullName, typeof(DateTime) },
            { typeof(TimeSpan).FullName, typeof(TimeSpan) },
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

            var strongTypeAttributeSyntax = typeDeclarationSyntax.AttributeLists
                .SelectMany(x => x.Attributes)
                .FirstOrDefault(x => receiver.IsStrongTypeAttribute(x));
            if (strongTypeAttributeSyntax is null)
            {
                _log.Add($"Unable to to find '[{nameof(StrongTypeAttribute)}]' attribute on type '{originalTypeName}'. Ignoring");
                return false;
            }

            var innerType = ExtractInnerType(semanticModel, strongTypeAttributeSyntax);
            if (innerType is null)
            {
                _log.Add($"Unable to identify inner type for type '{originalTypeName}'. Ignoring");
                return false;
            }

            var extraFeatures = ExtractExtraFeatures2(semanticModel, typeDeclarationSyntax, strongTypeAttributeSyntax, innerType);

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

        private Type? ExtractInnerType(SemanticModel semanticModel, AttributeSyntax strongTypeAttributeSyntax)
        {
            // [StrongType()]
            //            ^
            var firstArgument = strongTypeAttributeSyntax.ArgumentList?.Arguments.FirstOrDefault();
            if (firstArgument is null) return DEFAULT_INNER_TYPE;

            // [StrongType(...))]
            //             ^
            if (firstArgument.Expression is not TypeOfExpressionSyntax typeOfExpressionSyntax) return DEFAULT_INNER_TYPE;

            // [StrongType(typeof(System.Guid, ...))]
            //             ^
            var typeSyntax = typeOfExpressionSyntax.Type;

            var typeSymbolInfo = semanticModel.GetTypeInfo(typeSyntax);
            var typeSymbol = typeSymbolInfo.Type;
            // [StrongType(typeof(System.Guid, ...))]
            //                    ^
            if (typeSymbol == null) return DEFAULT_INNER_TYPE;

            var typeName = typeSymbol.ToDisplayString();
            var result = GetTypeByName(typeName);
            return result;
        }

        private static Type? GetTypeByName(string? typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return null;

            if (KNOWN_TYPES.TryGetValue(typeName!, out var knownType))
            {
                return knownType;
            }

            var systemType = SYSTEM_PRIVATE_CORE_LIB_ASSEMBLY.GetType(typeName!);
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

        private ExtraFeatures ExtractExtraFeatures2(
            SemanticModel semanticModel,
            TypeDeclarationSyntax typeDeclarationSyntax,
            AttributeSyntax strongTypeAttributeSyntax,
            Type innerType)
        {
            var isAbstract = IsAbstract(typeDeclarationSyntax);

            var hasBaseType = HasBaseClass(semanticModel, typeDeclarationSyntax);

            var doesAllowEmpty = ExtractAllowEmpty(semanticModel, strongTypeAttributeSyntax);

            var innerTypeHasStringConstructor = InnerTypeHasStringConstructor(innerType);

            var hasStringConstructor = HasStringConstructor(semanticModel, typeDeclarationSyntax, innerTypeHasStringConstructor);

            var doGenerateStringConstructor = DoGenerateStringConstructor(innerType, innerTypeHasStringConstructor, hasStringConstructor);

            var methodDeclarationSyntaxes = typeDeclarationSyntax.Members
                .Where(x => x is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
                .ToArray();
            var hasIsValid = HasIsValid(semanticModel, methodDeclarationSyntaxes, innerType);
            var hasToString = HasToString(methodDeclarationSyntaxes);

            var result = new ExtraFeatures(
                isAbstract,
                hasBaseType,
                doesAllowEmpty,
                doGenerateStringConstructor,
                hasToString,
                hasIsValid);
            return result;
        }

        private IsAbstract IsAbstract(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return (IsAbstract)typeDeclarationSyntax.Modifiers.Any(x => Equals(x.Value, "abstract"));
        }

        private HasBaseClass HasBaseClass(SemanticModel semanticModel, TypeDeclarationSyntax typeDeclarationSyntax)
        {
            if (typeDeclarationSyntax.BaseList?.Types is null) return (HasBaseClass)false;

            foreach (var baseTypeSyntax in typeDeclarationSyntax.BaseList.Types)
            {
                var typeKind = semanticModel.GetTypeInfo(baseTypeSyntax.Type).Type?.TypeKind;
                if (typeKind == TypeKind.Class)
                {
                    return (HasBaseClass)true;
                }
            }

            return (HasBaseClass)false;
        }

        private DoesAllowEmpty ExtractAllowEmpty(SemanticModel semanticModel, AttributeSyntax strongTypeAttributeSyntax)
        {
            // [StrongType(..., allow: Allow.Empty)]
            //             ^ Any arguments
            var attributeArgumentSyntaxes = strongTypeAttributeSyntax.ArgumentList?.Arguments;
            if (attributeArgumentSyntaxes is null || attributeArgumentSyntaxes.Value.Count == 0) return (DoesAllowEmpty)false;

            // [StrongType(..., allow: Allow.Empty)]
            //  ^ Constructor method
            var attributeSymbol = semanticModel.GetSymbolInfo(strongTypeAttributeSyntax);
            var methodSymbol = attributeSymbol.Symbol as IMethodSymbol;

            var result = HasEnumArgument(semanticModel, methodSymbol!, attributeArgumentSyntaxes, Allow.Empty);
            return (DoesAllowEmpty)result;
        }

        private InnerTypeHasStringConstructor InnerTypeHasStringConstructor(Type innerType)
        {
            return (InnerTypeHasStringConstructor)(innerType.GetConstructor(new[] { typeof(string) }) != null);
        }

        private HasStringConstructor HasStringConstructor(
            SemanticModel semanticModel,
            TypeDeclarationSyntax typeDeclarationSyntax,
            InnerTypeHasStringConstructor innerTypeHasStringConstructor)
        {
            if (innerTypeHasStringConstructor && typeDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                var constructorDeclarationSyntaxes = classDeclarationSyntax.Members
                    .Where(x => x is ConstructorDeclarationSyntax)
                    .Cast<ConstructorDeclarationSyntax>();
                if (constructorDeclarationSyntaxes.Any(x => HasSingleParameter(semanticModel, x, typeof(string))))
                {
                    return (HasStringConstructor)true;
                }
            }

            return (HasStringConstructor)false;
        }

        private DoGenerateStringConstructor DoGenerateStringConstructor(
            Type innerType,
            InnerTypeHasStringConstructor innerTypeHasStringConstructor,
            HasStringConstructor hasStringConstructor)
        {
            return (DoGenerateStringConstructor)(innerType != typeof(string) && innerTypeHasStringConstructor && !hasStringConstructor);
        }

        private HasIsValid HasIsValid(SemanticModel semanticModel, MethodDeclarationSyntax[] methodDeclarationSyntaxes, Type innerType)
        {
            return (HasIsValid)methodDeclarationSyntaxes.Any(x =>
                HasSingleParameter(semanticModel, x, innerType)
                && HasNameAndReturnType(x, "IsValid", "bool"));
        }

        private HasToString HasToString(MethodDeclarationSyntax[] methodDeclarationSyntaxes)
        {
            return (HasToString)methodDeclarationSyntaxes.Any(x => x.Identifier.Text == "ToString" && x.ParameterList.Parameters.Count == 0);
        }

        private bool HasEnumArgument<TEnum>(
            SemanticModel semanticModel,
            IMethodSymbol methodSymbol,
            IReadOnlyCollection<AttributeArgumentSyntax> attributeArgumentSyntaxes,
            TEnum enumValue)
                where TEnum : Enum
        {
            // [StrongType((Allow)1)]
            // [StrongType((Allow)2)]
            // [StrongType((Allow)3)]
            // NOTE: We don't support such exotic values. Why a developer would do this?

            // [StrongType(..., allow: Allow.Null)]
            //                  ^
            var hasParameterOfType = methodSymbol!.Parameters.Any(x => x.Type.ToDisplayString() == typeof(TEnum).FullName);
            if (!hasParameterOfType) return false;

            // [StrongType(..., allow: Allow.Null)]
            //             ^    ^
            foreach (var argumentSyntax in attributeArgumentSyntaxes)
            {
                // [StrongType(..., allow: Allow.Null)]
                //                         ^
                var expressionSyntax = argumentSyntax.Expression;

                // [StrongType(..., allow: Allow.Null | Allow.Empty)]
                //                                    ^
                if (expressionSyntax is BinaryExpressionSyntax binaryExpressionSyntax)
                {
                    // [StrongType(..., allow: Allow.Null | Allow.Empty)]
                    //                         ^
                    var binaryOperation = semanticModel.GetOperation(binaryExpressionSyntax);
                    if (binaryOperation?.Type?.ToDisplayString() == typeof(TEnum).FullName)
                    {
                        // [StrongType(..., allow: Allow.Null | Allow.Empty)]
                        //                         ^
                        // Will be `null` for `Allow.Null | Allow.Empty | Allow.Empty` value
                        // NOTE: We don't support such exotic values. Why a developer would do this?
                        var left = binaryExpressionSyntax.Left as MemberAccessExpressionSyntax;
                        // [StrongType(..., allow: Allow.Null | Allow.Empty)]
                        //                                      ^
                        var right = binaryExpressionSyntax.Right as MemberAccessExpressionSyntax;

                        // [StrongType(..., allow: Allow.Null)]
                        //                               ^
                        var result = IsEnumValue(semanticModel, left, enumValue)
                            || IsEnumValue(semanticModel, right, enumValue);
                        return result;
                    }
                }

                // [StrongType(..., allow: Allow.Null)]
                //                         ^
                if (expressionSyntax is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
                {
                        // [StrongType(..., allow: Allow.Null)]
                        //                               ^
                        var result = IsEnumValue(semanticModel, memberAccessExpressionSyntax, enumValue);
                        return result;
                }
            }

            return false;
        }

        private bool IsEnumValue<TEnum>(
            SemanticModel semanticModel,
            MemberAccessExpressionSyntax? memberAccessExpressionSyntax,
            TEnum value)
            where TEnum : Enum
        {
            if (memberAccessExpressionSyntax is null) return false;

            // [StrongType(..., allow: Allow.Null)]
            //                              ^
            var symbolInfo = semanticModel.GetSymbolInfo(memberAccessExpressionSyntax);
            // [StrongType(..., allow: Allow.Null)]
            //                         ^
            var result = symbolInfo.Symbol?.ContainingType.ToDisplayString() == typeof(TEnum).FullName
                // [StrongType(..., allow: Allow.Null)]
                //                               ^
                && symbolInfo.Symbol?.Name == value.ToString();
            return result;
        }

        private static bool HasSingleParameter(
            SemanticModel semanticModel,
            BaseMethodDeclarationSyntax methodDeclarationSyntax,
            Type expectedType)
        {
            // constructor(...)
            //             ^
            if (methodDeclarationSyntax.ParameterList.Parameters.Count != 1) return false;

            // constructor(string value)
            //             ^
            var firstParameter = methodDeclarationSyntax.ParameterList.Parameters.First();
            var symbolInfo = semanticModel.GetTypeInfo(firstParameter.Type!);
            // constructor(string value)
            //             ^
            var typeName = symbolInfo.Type?.ToDisplayString();
            var type = GetTypeByName(typeName);
            if (type == null) return false;

            var result = type == expectedType;
            return result;
        }

        private static bool HasNameAndReturnType(
            MethodDeclarationSyntax methodDeclarationSyntax,
            string expectedName,
            string expectedReturnType)
        {
            var result = methodDeclarationSyntax.Identifier.Text == expectedName
                && methodDeclarationSyntax.ReturnType.ToString() == expectedReturnType;
            return result;
        }
    }
}
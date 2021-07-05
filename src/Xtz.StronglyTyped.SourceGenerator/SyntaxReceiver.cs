using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xtz.StronglyTyped.SourceGenerator
{
    /// <summary>
    /// This is used to process the syntax tree. The output is "work items", which are fed into the code generators.
    /// </summary>
    /// <remarks>
    /// Created on demand before each generation pass
    /// </remarks>
    public class SyntaxReceiver : IStrongTypeSyntaxReceiver
    {
        private static readonly string STRONGLY_TYPED_ATTRIBUTE_CLASS = nameof(StrongTypeAttribute).Substring(0, nameof(StrongTypeAttribute).Length - 9);

        private readonly List<string> _log = new();
        
        private readonly List<StrongTypeDeclaration> _declarations = new();

        public IReadOnlyCollection<string> Log => _log;

        public IReadOnlyCollection<StrongTypeDeclaration> Declarations => _declarations;

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        [ExcludeFromCodeCoverage]
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            try
            {
                if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
                {
                    var hasStrongTypeAttribute = HasStrongTypeAttribute(typeDeclarationSyntax);
                    if (!hasStrongTypeAttribute) return;

                    var declaration = new StrongTypeDeclaration(typeDeclarationSyntax);
                    _declarations.Add(declaration);
                    _log.Add($"Found a type '{typeDeclarationSyntax.Identifier}' ({typeDeclarationSyntax.Kind()}). Parent syntax: '{(typeDeclarationSyntax.Parent as NamespaceDeclarationSyntax)?.Name}'");
                }
            }
            catch (Exception ex)
            {
                _log.Add($"Error parsing syntax: {ex.Message}");
            }
        }

        private bool HasStrongTypeAttribute(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var result = typeDeclarationSyntax.AttributeLists
                .SelectMany(x => x.Attributes)
                .Any(x => IsStrongTypeAttribute(x));
            return result;
        }

        public bool IsStrongTypeAttribute(AttributeSyntax attributeSyntax)
        {
            var nameSyntax = attributeSyntax.Name is QualifiedNameSyntax qualifiedNameSyntax
                ? qualifiedNameSyntax.Right
                : attributeSyntax.Name;
            var result = (nameSyntax as IdentifierNameSyntax)?.Identifier.ValueText == STRONGLY_TYPED_ATTRIBUTE_CLASS;
            return result;
        }
   }
}
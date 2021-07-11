using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    public record StrongTypeDeclaration(TypeDeclarationSyntax TypeDeclarationSyntax);
}